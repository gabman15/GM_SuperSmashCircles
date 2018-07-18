game = nil
entity = nil
user = nil
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
function OnLink(u)
	user = u
end
function OnUpdate()
	if user ~= nil then
		if user.Input:GetRight() then
			entity.DX = entity.DX + 1
		end
		if user.Input:GetLeft() then
			entity.DX = entity.DX - 1
		end
		if user.Input:GetJump() then
			entity.DY = -5 --no ground checks for now
		end
	end
end