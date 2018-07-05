-- Circle class for each player

Circle = Class{}

function Circle:init(x, y, width, height, type)
    self.x = x
    self.y = y
    self.width = width
    self.height = height
    --[[ Which type of character the circle is
        0 - Red
        1 - Pink

        BETA
        probably different names for characters and there will be more characters
    ]]
    self.type = type

    -- Speed of Circle
    self.dy = 0
    self.dx = 0
end