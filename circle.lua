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
    --Number of Jumps circle has
    self.jumps = 2
    self.jumping = false
    self.canJump = true
    -- Default type is 0 (Red)
    self.char = RED

    -- Lives of the character (default starts at 5)
    self.lives = 5

    -- Speed of Circle
    self.dy = 0
    self.dx = 0

    --Whether the circle is alive
    self.living = true

    self.timer = 0
end

function Circle:init(x, y, controls, joystick)
    self.x = x
    self.y = y
    self.width = 30
    self.height = 30
    self.controls = controls
    self.joystick = joystick
    
    --Number of Jumps circle has
    self.jumps = 2
    self.jumping = false
    self.canJump = true
    -- Default type is 0 (Red)
    self.char = RED

    -- Lives of the character (default starts at 5)
    self.lives = 5

    -- Speed of Circle
    self.dy = 0
    self.dx = 0

    --Whether the circle is alive
    self.living = true

    self.timer = 0
end

function Circle:setType(char)
    self:init()
    self.char = char
end

function Circle:update(dt)
    if(self.timer > 0) then
        self.timer = self.timer - 1
    end

    self.jumping = false
    self.x = self.x + self.dx * dt
    self.y = math.min(WINDOW_HEIGHT-150, self.y + self.dy * dt)

    if(self.y == WINDOW_HEIGHT-150) then
        self.jumps = 2
        
    end
    if(self.dx > 0) then
        self.dx = self.dx - self.dx * XGRAVITY
    end

    if(self.dx < 0) then
        self.dx = self.dx + self.dx * -XGRAVITY
    end

    if(self.x < -10) or (self.x > WINDOW_WIDTH + 10) then
        self:die()
    end

    self.dy = self.dy + YGRAVITY
end

function Circle:action(key)
    if(self.living) then
        if key == self.controls['jump'] and self.canJump and not self.jumping and self.jumps > 0 and self.timer == 0 then
            self.dy = math.min(- 250, self.dy - 250)
            self.jumps = self.jumps - 1
            self.jumping = true
            self.timer = 15
            --self.canJump = false
        elseif(key == self.controls['left']) then
            self.dx = self.dx - 30
        elseif(key == self.controls['right']) then
            self.dx = self.dx + 30
        elseif(key == self.controls['down']) then
            self.dy = self.dy + 30
        end
    end
end

function Circle:isJumping()
    if(self.jumping) then
        return "true"
    end
    return "false"
end

function Circle:die()
    self.lives = self.lives - 1
    if self.lives == 0 then
        self.living = false
    end
    if(self.living) then
        self.x = WINDOW_WIDTH/2
        self.y = WINDOW_HEIGHT/2
        self.dx = 0
        self.dy = 0
        self.jumps = 2
        self.jumping = false
        self.canJump = true
    end
end

function Circle:render()
    if(self.living) then
        love.graphics.setColor(255,0,0)
        love.graphics.circle('fill', players[1].x, players[1].y, players[1].width)
    end
end