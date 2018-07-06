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

    --CONTROLS
    LEFT_KEYBOARD = {
        ['jump'] = 'w',
        ['up'] = 'w',
        ['down'] = 's',
        ['left'] = 'a',
        ['right'] = 'd',
        ['dash'] = 'h',
        ['special'] = 'j'
    }

    RIGHT_KEYBOARD = {
        ['jump'] = 'up',
        ['up'] = 'up',
        ['down'] = 'down',
        ['left'] = 'left',
        ['right'] = 'right',
        ['dash'] = '+',
        ['special'] = '-'
    }

    --PLAYERS (DEV TEST)
    players = {
        [1] = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, LEFT_KEYBOARD),
        [2] = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, RIGHT_KEYBOARD)
    }

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
                if love.keyboard.isDown(value) then
                    players[i]:action(value)
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
            if(players[i].controls["up"] == key) then
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
        
    elseif gameState == PLAYING then
        love.graphics.setFont(smallFont)
        love.graphics.printf("PLAYER 1: \n" .. players[1].lives, -500, 50, WINDOW_WIDTH, 'center')
        
        love.graphics.printf("PLAYER 2: \n" .. players[2].lives, 500, 50, WINDOW_WIDTH, 'center')
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