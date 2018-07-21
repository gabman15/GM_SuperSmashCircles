import("System");
game = nil
entity = nil
user = nil
movementAcceleration = 4
maxXSpeed = 5
maxYSpeed = 10
jumpAmount = -9;
jumpsLeft = 2
jumpPressedLast = false
function OnCreation(en, g)
	game = g
	entity = en
	entity.X = 64
	entity.Y = 64
	entity.Width = 32
	entity.Height = 32
	entity.RepelAmount = 0.4
	entity.Friction = 1;
	entity:SetImage("circle")
	entity:SetColor(255, 0, 0)
end
function OnLink(u)
	user = u
end
function OnUpdate()
	if user ~= nil then
		if entity.DX < maxXSpeed and user.Input:Get("right") then
			entity.DX = entity.DX + movementAcceleration
		end
		if entity.DX > -maxXSpeed and user.Input:Get("left") then
			entity.DX = entity.DX - movementAcceleration
		end
		jump = user.Input:Get("jump")
		if jump and not jumpPressedLast and jumpsLeft > 0 then
			entity.DY = jumpAmount
			jumpsLeft = jumpsLeft - 1
		end
		jumpPressedLast = jump
		if user.Input:Get("down") then
			entity.CollideWithPlatforms = false
		else
			entity.CollideWithPlatforms = true
		end
	end
	--wont do the following for now until there is a better way
	--if entity.DY > maxYSpeed then
	--	entity.DY = maxYSpeed
	--elseif entity.DY < -maxYSpeed then
	--	entity.DY = -maxYSpeed
	--end
end
function OnSolidCollision()
	jumpsLeft = 2
end
function OnEntityCollision()
end