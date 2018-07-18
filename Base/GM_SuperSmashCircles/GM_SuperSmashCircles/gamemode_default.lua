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
	game.YGravity = 0.1
	ent = game:CreateEntity("player.lua")
	plt = game:CreatePlatform("testplatform.lua")
	plt = game:CreatePlatform("testplatform.lua")
	plt.X = plt.X + 32
	plt = game:CreatePlatform("testplatform.lua")
	plt.X = plt.X + 64
	plt = game:CreatePlatform("testplatform.lua")
	plt.X = plt.X + 96
	plt = game:CreatePlatform("testplatform.lua")
	plt.X = plt.X + 128
	game.Users:GetValue(0):LinkEntity(ent)
end
function OnUpdate()
	--Console.WriteLine("update")
end