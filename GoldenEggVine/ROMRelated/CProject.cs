using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GoldenEggVine.ROMRelated.LevelRelated.LevelEntities;

namespace GoldenEggVine.ROMRelated
{
	/// <summary>
	/// YI Hacking Project
	/// Projects are used for easily organizing data that belongs to a hacking project
	/// The content of a hack is not loaded directly from the ROM, but instead from the project files
	/// 
	/// The idea is to insert small amounts of data for testing into a copy of the base ROM,
	/// which can be adjusted to include only certain levels and ASM patches for example, and all of the data for releases
	/// </summary>
	class CProject
	{
		private string _projectRoot;
		private string _projectName;

		private const string _levelsFolder = "Levels";
		private const string _entrancesFolder = "Entrances";
		private const string _midringsFolder = "MiddleRings";
		private const string _graphicsFolder = "Graphics";
		private const string _asmFolder = "ASM";
		private const string _labelsFolder = "Labels";
		private const string _baseRom = "Base.smc";
		private const string _emulatorPath = "EmulatorPath.txt";

		private CYIROM _rom;

		public string Root { get { return _projectRoot; } }
		public string LevelsFolder { get { return Path.Combine(_projectRoot, _levelsFolder); } }
		public string EntrancesFolder { get { return Path.Combine(_projectRoot, _entrancesFolder); } }
		public string MidringsFolder { get { return Path.Combine(_projectRoot, _midringsFolder); } }
		public string GraphicsFolder { get { return Path.Combine(_projectRoot, _graphicsFolder); } }
		public string AsmFolder { get { return Path.Combine(_projectRoot, _asmFolder); } }
		public string LabelsFolder { get { return Path.Combine(_projectRoot, _labelsFolder); } }
		public string BaseRom { get { return Path.Combine(_projectRoot, _baseRom); } }
		public string EmulatorPath { get { return Path.Combine(_projectRoot, _emulatorPath); } }

		public CProject(string projectName, string parentDir)
		{
			_projectRoot = Path.Combine(parentDir, projectName);
			_projectName = projectName;
		}

		public CProject(string projectPath)
		{
			if(Directory.Exists(projectPath))
			{
				_projectRoot = projectPath;
				_rom = new CYIROM(BaseRom);
			}
			else
			{
				throw new DirectoryNotFoundException("The project directory could not be found!");
			}
		}

		public void InitialSetup(string baseRomPath)
		{
			if(Directory.Exists(Root))
			{
				throw new ArgumentException("The project directory already exists, recreating everything could be harmful");
			}

			try
			{
				// TODO Generate project data files
				// => Extract graphics, Entrances, Middle Rings and Levels from base ROM
				// The data is then loaded by Golden EggVine as soon as a project is opened

				Directory.CreateDirectory(Root);
				File.Copy(baseRomPath, BaseRom);
				File.Create(EmulatorPath);

				Directory.CreateDirectory(LevelsFolder);
				Directory.CreateDirectory(EntrancesFolder);
				Directory.CreateDirectory(MidringsFolder);
				Directory.CreateDirectory(AsmFolder);
				Directory.CreateDirectory(LabelsFolder);

				var rom = new CYIROM(BaseRom);
				var levelData = rom.GetAllLevelData();
				for(int i = 0; i <= 0xDD; ++i)
				{
					File.WriteAllBytes(
						Path.Combine(LevelsFolder, String.Format("{0:X2}.obj", i)),
						rom.GetLevelBytes(rom.GetLevelSpecs(i, ELevelData.OBJECTS))
					);
					File.WriteAllBytes(
						Path.Combine(LevelsFolder, String.Format("{0:X2}.spr", i)),
						rom.GetLevelBytes(rom.GetLevelSpecs(i, ELevelData.SPRITES))
					);
				}


				var midwaysByTransLevel = rom.GetMidwayData().ToDictionary(x => x.TransLevel);
				var entrancesByTransLevel = rom.GetEntranceData().ToDictionary(x => x.TransLevel);
				for (int i = 0; i < CYIROM.NumTransLevels; ++i)
				{
					var entranceData = entrancesByTransLevel[i];
					File.WriteAllBytes(
						Path.Combine(EntrancesFolder, String.Format("{0:X2}.ent", i)),
						entranceData.GetData()
					);

					var levels = entranceData.GetAllReachableLevels(rom);
					var midwayData = midwaysByTransLevel[i];

					using (BinaryWriter binWriter = new BinaryWriter(
						File.OpenWrite(Path.Combine(MidringsFolder, String.Format("{0:X2}.mwd", i)))
					))
					{
						for (int j = 0; j < 4; ++j)
						{
							if (levels.Contains(midwayData.GetLevel(j)))
								binWriter.Write(midwayData.GetData(j));
						}
						binWriter.Close();
					}
				}
			}
			catch(Exception e)
			{
				// TODO Sensible catch (note we may be mid-creation here and thus have created/deleted some stuff already and may want to undo things in a reasonable way)
				System.Windows.Forms.MessageBox.Show(e.ToString());
			}

		}
	}
}
