import 'GameServer'
import 'GameServer.Data'

local Priest = Class();
Priest.Id = 1;
Priest.Name = 'Padre';
Priest.Selectable = true;

Priest.Level = 1;
Priest.Experience = 0;
Priest.Points = 0;

Priest.MapId = 1;
Priest.X = 2;
Priest.Y = 3;
 
Priest.MaleSprite[0] = 1;
Priest.MaleSprite[1] = 2;
Priest.MaleSprite[2] = 3;

Priest.FemaleSprite[0] = 4;
Priest.FemaleSprite[1] = 5;
Priest.FemaleSprite[2] = 6;

AddClass(Priest);