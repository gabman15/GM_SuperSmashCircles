import("System")
import("GM_SuperSmashCircles")
gamemode = nil
game = nil
colorOrderPosition = 0
function OnCreation(gm, g)
	Console.WriteLine("new gamemode")
	gamemode = gm
	game = g
end
function OnStart()
	Console.WriteLine("gamemode started")
	game.YGravity = 0.5
	game.AirFriction = 0
	plt = game:CreatePlatform("platform.lua")
	plt.Width = 256
	plt = game:CreatePlatform("platform.lua")
	plt.Width = 288
	plt.Y = plt.Y - 128
	plt = game:CreatePlatform("platform.lua")
	plt.Width = 320
	plt.Y = plt.Y + 128
end
function OnUpdate()
	--Console.WriteLine("update")
end
function OnNewUser(user)
	ent = game:CreateEntity("player.lua")
	nextCol = GetNextColor()
	if nextCol ~= nil then
		ent.Color = nextCol
	else
		ent:SetColor(0, 255, 0)
	end
	user:LinkEntity(ent)
end
function GetNextColor()
	if game ~= nil then
		col = game.UserColorOrder:ToArray():GetValue(colorOrderPosition)
		colorOrderPosition = colorOrderPosition + 1
		return col
	end
	return nil
end