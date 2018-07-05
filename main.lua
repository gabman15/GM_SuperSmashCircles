--[[
    Super Smash Circles Remade in Lua

    by Gabriel McNinch
    Based on the popular Super Smash Bros Series by Nintendo

    Original Super Smash Circles by Nate Werst
    Remade again by Gabriel McNinch and Cameron McIlvenna
]]

-- https://github.com/vrld/hump/blob/master/class.lua
Class = require 'class'

-- Requires Circle class
require 'Circle'

-- Size of the window
WINDOW_WIDTH = 1280
WINDOW_HEIGHT = 720

-- Game States
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

    player1 = Circle(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2)

    gameState = SETUP
end

function love.draw()
    if(gameState == SETUP)
        love.graphics.printf("SUPER SMASH CIRCLES", 0, WINDOW_HEIGHT / 2, WINDOW_WIDTH, "center")

        love.graphics.printf()
    end

end