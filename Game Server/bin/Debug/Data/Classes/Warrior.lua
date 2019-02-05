import 'GameServer'
import 'GameServer.Data'

local Warrior = Class();
Warrior.Id = 2;
Warrior.Name = 'Guerreiro';
Warrior.Selectable = true;

Warrior.Level = 1;
Warrior.Experience = 0;
Warrior.Points = 0;

Warrior.MapId = 1;
Warrior.X = 2;
Warrior.Y = 3;

Warrior.MaleSprite[0] = 1;
Warrior.MaleSprite[1] = 2;
Warrior.MaleSprite[2] = 3;

Warrior.FemaleSprite[0] = 4;
Warrior.FemaleSprite[1] = 5;
Warrior.FemaleSprite[2] = 6;

AddClass(Warrior);