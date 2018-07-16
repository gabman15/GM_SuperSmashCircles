import("System")
gamemode = nil
game = nil
function OnCreation(gm, g)
	Console.WriteLine("new gamemode")
	gamemode = gm
	game = g
	game.Gravity = 0.1
	game:CreateEntity("testentity.lua")
end
function OnStart()
	Console.WriteLine("gamemode started")
end
function OnUpdate()
	--Console.WriteLine("update")
end