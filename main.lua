--[[
    Super Smash Circles Remade in Lua

    by Gabriel McNinch
    Based on the popular Super Smash Bros Series by Nintendo

    Original Super Smash Circles by Nate Werst
    Remade again by Gabriel McNinch and Cameron McIlvenna
]]

-- https://github.com/vrld/hump/blob/master/class.lua
require 'class'

-- Requires Circle class
require 'Circle'

-- Size of the window
WINDOW_WIDTH = 1280
WINDOW_HEIGHT = 720

-- GAME STATES
SETUP = 0
PLAYING = 1
OVER = 2

-- Gravity within game
YGRAVITY = 5
XGRAVITY = 0.035

function love.load()
    love.window.setMode(WINDOW_WIDTH, WINDOW_HEIGHT, {
        vsync = true,
        fullscreen = false,
        resizable = true
    })

    love.window.setTitle("Super Smash Circles")


    print("test")
    --FONTS
    smallFont = love.graphics.newFont(20)
    largeFont = love.graphics.newFont(50)

    love.joystick.loadGamepadMappings("/controls/gamecontrollerdb.txt")

    --CONTROLS
    LEFT_KEYBOARD = {
        ['jump'] = 'space',
        ['up'] = 'w',
        ['down'] = 's',
        ['left'] = 'a',
        ['right'] = 'd',
        ['dash'] = 'h',
        ['special'] = 'j'
    }

    RIGHT_KEYBOARD = {
        ['jump'] = '*',
        ['up'] = 'up',
        ['down'] = 'down',
        ['left'] = 'left',
        ['right'] = 'right',
        ['dash'] = '+',
        ['special'] = '-'
    }

    XBOX = {
        ['jump'] = 'x',
        ['up'] = 'dpup',
        ['down'] = 'dpdown',
        ['left'] = 'dpleft',
        ['right'] = 'dpright',
        ['dash'] = 'a',
        ['special'] = 'b'
    }

    GAMECUBE = {
        ['jump'] = 'x',
        ['up'] = 'dpup',
        ['down'] = 'dpdown',
        ['left'] = 'dpleft',
        ['right'] = 'dpright',
        ['dash'] = 'a',
        ['special'] = 'b'

    }

    

    joysticks = love.joystick.getJoysticks()

    --PLAYERS (DEV TEST)
    players = {
        [1] = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, XBOX, joysticks[4]),
        [2] = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, RIGHT_KEYBOARD)
    }

    if(players[1].joystick:getName() == 'MAYFLASH GameCube Controller Adapter') then
        love.joystick.setGamepadMapping(players[1].joystick:getGUID(), 'x', 'button', 1)
    end

    love.graphics.AlignMode = 'center'
    gameState = SETUP
    love.keyboard.keysPressed = {}
end

function love.update(dt)
    if gameState == SETUP then
        if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
            gameState = PLAYING
        end
    elseif gameState == PLAYING then
        for i = 1, table.maxn(players), 1 do
            players[i]:update(dt)
            
            for _, value in pairs(players[i].controls) do
                if players[i].joystick ~= null then
                    if players[i].joystick:isGamepad() then
                        if players[i].joystick:isGamepadDown(value) then
                            players[i]:action(value)
                        end
                    else
                        if players[i].joystick:isDown(value) then
                            players[i]:action(value)
                        end
                    end
                else
                    if love.keyboard.isDown(value) then
                        players[i]:action(value)
                    end
                end
            end
        end

        
    end

    love.keyboard.keysPressed = {}
end

function love.keypressed(key)
    if (key == 'escape') then
        love.event.quit()
    end

    love.keyboard.keysPressed[key] = true
end

function love.keyreleased(key)
    if gameState == PLAYING then
        for i = 1, table.maxn(players), 1 do
            if(players[i].controls["jump"] == key) then
                players[i].canJump = true
            end
        end
    end
end

function love.joystickreleased(joystick, button)
    print("Joystick "..joystick:getName().." released "..button)
    if gameState == PLAYING then
        for i = 1, table.maxn(players), 1 do
            print(players[i].controls['jump'])
            if players[i].joystick == joystick and players[i].controls["jump"] == button then
                players[i].canJump = true
            end
        end
    end
end

function love.gamepadreleased(joystick, button)
    print("Joystick "..joystick:getName().." released "..button)
    if gameState == PLAYING then
        for i = 1, table.maxn(players), 1 do
            if players[i].joystick == joystick and players[i].controls["jump"] == button then
                players[i].canJump = true
            end
        end
    end
end


function love.keyboard.wasPressed(key)
    if love.keyboard.keysPressed[key] then
        return true
    else
        return false
    end
end

function love.draw()
    love.graphics.clear()
    love.graphics.setColor(255,255,255)
    love.window.setTitle("Super Smash Circles (" .. love.timer.getFPS() .. " fps)")
    if gameState == SETUP then
        love.graphics.setFont(largeFont)
        love.graphics.printf("SUPER SMASH CIRCLES", 0, WINDOW_HEIGHT / 2, WINDOW_WIDTH, "center")
        love.graphics.printf("Press Enter to play!", 0, WINDOW_HEIGHT / 2 + 100, WINDOW_WIDTH, "center")
        love.graphics.setFont(smallFont)
        love.graphics.print("JOYSTICKS:", 100,100)
        for i = 1, table.maxn(joysticks), 1 do
            love.graphics.print(joysticks[i]:getName(), 100,100 + 100*i)
        end
        
    elseif gameState == PLAYING then
        love.graphics.setFont(smallFont)
        love.graphics.printf("PLAYER 1: \n" .. players[1]:isJumping(), -500, 50, WINDOW_WIDTH, 'center')
        
        love.graphics.printf("PLAYER 2: \n" .. players[1].jumps, 500, 50, WINDOW_WIDTH, 'center')
        for i = 1, table.maxn(players), 1 do
            players[i]:render()
        end
    end
end

function table.contains(table, element)
    for _, value in pairs(table) do
      if value == element then
        return true
      end
    end
    return false
end