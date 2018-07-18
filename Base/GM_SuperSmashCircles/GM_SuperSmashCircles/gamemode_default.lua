import("System")
import("GM_SuperSmashCircles")
gamemode = nil
game = nil
function OnCreation(gm, g)
	Console.WriteLine("new gamemode")
	gamemode = gm
	game = g
end
function OnStart()
	Console.WriteLine("gamemode started")
	game.YGravity = 0.5
	game.AirFriction = 0
	ent = game:CreateEntity("player.lua")
	plt = game:CreatePlatform("platform.lua")
	plt.Width = 256
	plt = game:CreatePlatform("platform.lua")
	plt.Width = 288
	plt.Y = plt.Y - 128
	game.Users:GetValue(0):LinkEntity(ent)
end
function OnUpdate()
	--Console.WriteLine("update")
end