import 'GameServer'
import 'GameServer.Data'

local Wizzard = Class();
Wizzard.Id = 3;
Wizzard.Name = 'Bruxo';
Wizzard.Selectable = true;

Wizzard.Level = 1;
Wizzard.Experience = 0;
Wizzard.Points = 0;

Wizzard.MapId = 1;
Wizzard.X = 2;
Wizzard.Y = 3;

Wizzard.MaleSprite[0] = 1;
Wizzard.MaleSprite[1] = 2;
Wizzard.MaleSprite[2] = 3;

Wizzard.FemaleSprite[0] = 4;
Wizzard.FemaleSprite[1] = 5;
Wizzard.FemaleSprite[2] = 6;

AddClass(Wizzard);