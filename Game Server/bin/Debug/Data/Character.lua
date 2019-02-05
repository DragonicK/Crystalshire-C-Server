Character = {
	-- Ativa ou desativa a criação de personagens.
	Creation = true,

	-- Ativa ou desativa a exclusão de personagens.
	Delete = true,

	-- Leve mínimo para exclusão.
	DeleteMinLevel = 1,

	-- Level máximo para exclusão.
	DeleteMaxLevel = 50,

	-- Impede que outras sprites sejam escolhidas por edição de pacotes.
	-- O usuário somente pode escolher entre o valor mínimo e máximo.
	
	-- Limite mínimo de uso de sprites. 
	SpriteRangeMinimum = 1,

	-- Lmite máximo de uso de sprites.
	SpriteRangeMaximum = 70
};

FirstExample = {
	LevelMin = 0,
	LevelMax = 50,
	Minutes = 1
};

-- Level Min, Level Max, Tempo em minutos.
-- Adiciona o tempo de exclusão do personagem.
AddTime(FirstExample.LevelMin, FirstExample.LevelMax, FirstExample.Minutes);

SecondExample = {
	LevelMin = 51,
	LevelMax = 100,
	Minutes = 1
};

--
AddTime(SecondExample.LevelMin, SecondExample.LevelMax, SecondExample.Minutes);