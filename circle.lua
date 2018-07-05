-- Circle class for each player

Circle = Class{}

-- Character Types
RED = 0
PINK = 1

function Circle:init(x, y)
    self.x = x
    self.y = y
    self.width = 30
    self.height = 30
    
    -- Default type is 0 (Red)
    self.type = RED

    -- Lives of the character (default starts at 5)
    self.lives = 5

    -- Speed of Circle
    self.dy = 0
    self.dx = 0
end

function Circle:setType(type)
    self.type = type
end

function Circle:reset(x, y)
    self.x = x
    self.y = y
    lives = lives - 1
end