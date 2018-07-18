import("System")
game = nil
platform = nil
function OnCreation(pl, g)
	game = g
	platform = pl
	platform.X = 64
	platform.Y = 256
	platform.Width = 32
	platform.Height = 16
	platform:SetImage("platform")
	platform:SetColor(0, 0, 0);
end
function OnCollision()
--	Console.WriteLine("collision")
end
function OnUpdate()
--
end