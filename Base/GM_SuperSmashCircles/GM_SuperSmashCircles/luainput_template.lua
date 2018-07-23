--variables for the game and input objects
game = nil
inp = nil
function OnCreation(i, g)
	--triggered soon after the lua input module object is created
	game = g
	inp = i
	--specifying deadzone for fractional input like joystick input
	deadzone = 0.5
	inp.InputItems:Add(inp:GenerateLuaInput("jump", 0))
	inp.InputItems:Add(inp:GenerateLuaInput("up", deadzone))
	inp.InputItems:Add(inp:GenerateLuaInput("down", deadzone))
	inp.InputItems:Add(inp:GenerateLuaInput("left", deadzone))
	inp.InputItems:Add(inp:GenerateLuaInput("right", deadzone))
	inp.InputItems:Add(inp:GenerateLuaInput("dash", 0))
	inp.InputItems:Add(inp:GenerateLuaInput("special", 0))
end
function OnUpdate()
	--if you need to update whatever does the input
	--this is triggered every game update
end
function Get(name)
	--if youre getting fractional input like from a joystick, return the fractional input and not a true or false
	--if no fractional input, use boolean output
end