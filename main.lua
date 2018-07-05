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
GRAVITY = 0.3

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
    player1 = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, LEFT_KEYBOARD)
    player2 = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, RIGHT_KEYBOARD)

    love.graphics.AlignMode = 'center'
    gameState = SETUP
end

function love.draw()
    love.graphics.clear()
    love.graphics.setColor(255,255,255)
    love.window.setTitle("Super Smash Circles (" .. love.timer.getFPS() .. " fps)")
    if gameState == SETUP then
        love.graphics.setFont(largeFont)
        love.graphics.printf("SUPER SMASH CIRCLES", 0, WINDOW_HEIGHT / 2, WINDOW_WIDTH, "center")
        love.graphics.setFont(smallFont)
        love.graphics.printf("PLAYER 1: \n" .. player1.lives, -500, 50, WINDOW_WIDTH, 'center')
        
        love.graphics.printf("PLAYER 2: \n" .. player2.lives, 500, 50, WINDOW_WIDTH, 'center')
        --player2.die(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2)
        love.graphics.printf("PLAYER 2: \n" .. player2.lives, 500, 600, WINDOW_WIDTH, 'center')

        love.graphics.setColor(255,0,0)
        love.graphics.circle('fill', player1.x, player1.y, player1.width)
    end

end