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
	entity.RepelAmount = 0.4
	entity:SetImage("circle")
	entity:SetColor(255, 0, 0)
end
function OnCollision()
	Console.WriteLine("collision")
end
function OnUpdate()
end
function OnEntityCollision()
end