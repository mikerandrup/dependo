(function (exports, Orange, Apple, Grapes) {

    'use strict';

    // cause exception if dependencies aren't available :)
    Orange.subthing;
    Apple.subthing;
    Grape.subthing;

}(this.Match, this.Match.Orange, this.Match.Apple, this.Match.Grapes));