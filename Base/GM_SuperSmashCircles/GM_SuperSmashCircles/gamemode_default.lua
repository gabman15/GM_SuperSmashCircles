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
	ent = game:CreateEntity("testentity.lua")
--	ent.Image = ContentManager.GetContent("circle");
end
function OnUpdate()
	--Console.WriteLine("update")
end