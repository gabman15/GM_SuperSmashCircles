import("System")
game = nil
entity = nil
function OnCreation(en, g)
	Console.WriteLine("I am alive.");
	game = g
	entity = en
	entity.X = 32
	entity.Y = 32
	entity.Width = 8
	entity.Height = 8
	entity.RepelAmount = 0
end
function OnCollision()
	Console.WriteLine("collision")
end
function OnUpdate()
	--Console.WriteLine("update")
end