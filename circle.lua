-- Circle class for each player

Circle = class()

-- Character Types
RED = 0
PINK = 1

function Circle:init(x, y, controls)
    self.x = x
    self.y = y
    self.width = 30
    self.height = 30
    self.controls = controls
    
    -- Default type is 0 (Red)
    self.char = RED

    -- Lives of the character (default starts at 5)
    self.lives = 5

    -- Speed of Circle
    self.dy = 0
    self.dx = 0

    --Whether the circle is alive
    self.living = true
end

function Circle:setType(char)
    self.char = char
end

function Circle:die()
    
    self.set(lives, lives - 1)
    if lives == 0 then
        living = false
    end
end