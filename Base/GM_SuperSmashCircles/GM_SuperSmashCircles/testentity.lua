import("System")
game = nil
entity = nil
function OnCreation(en, g)
	game = g
	entity = en
	entity.X = 64
	entity.Y = 64
	entity.Width = 32
	entity.Height = 32
	entity.RepelAmount = 0
	entity:SetImage("circle")
	entity:SetColor(255, 0, 0)
end
function OnCollision()
	Console.WriteLine("collision")
end
function OnUpdate()
--	if entity:OnGround() == true then
--		Console.WriteLine("on ground")
--	else
--		Console.WriteLine("flying through the air");
--	end
	--entity.X = entity.X + entity.DX
	--entity.Y = entity.Y + entity.DY
end