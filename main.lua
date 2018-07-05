--[[
    Super Smash Circles Remade in Lua

    by Gabriel McNinch
    Based on the popular Super Smash Bros Series by Nintendo

    Original Super Smash Circles by Nate Werst
    Remade again by Gabriel McNinch and Cameron McIlvenna
]]

-- https://github.com/vrld/hump/blob/master/class.lua
Class = require 'class'

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
    
    players = {}

    gameState = SETUP
end

function love.draw()
    love.graphics.print(GRAVITY, WINDOW_WIDTH/2, WINDOW_HEIGHT/2)

end