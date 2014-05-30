(function (exports, Orange, Apple, Fig) {

    'use strict';

    // cause exception if dependencies aren't available :)
    Orange.subthing;
    Apple.subthing;
    Fig.subthing;

}(this.Match, this.Match.Orange, this.Match.Apple, this.Match.Fig));