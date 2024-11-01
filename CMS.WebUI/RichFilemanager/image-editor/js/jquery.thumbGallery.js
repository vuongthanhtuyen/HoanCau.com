
/*
 * JQuery Thumbnail Gallery With LightBox by Tean v1.2
 * http://codecanyon.net/item/jquery-thumbnail-gallery-with-lightbox/2711906
 */



(function ($) {

    function ThumbGallery(element, options) {

        this._componentInited = false;
        var self = this;

        this.settings = $.extend({}, $.fn.thumbGallery.defaults, options);

        this.isMobile = (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent));
        //this.isMobile=true;
        //console.log(this.isMobile);

        //icons
        this.ic_thumb_forward = this.settings.ic_thumb_forward || 'data/icons/thumb_forward.png';
        this.ic_thumb_forward_on = this.settings.ic_thumb_forward_on || 'data/icons/thumb_forward_on.png';
        this.ic_thumb_backward = this.settings.ic_thumb_backward || 'data/icons/thumb_backward.png';
        this.ic_thumb_backward_on = this.settings.ic_thumb_backward_on || 'data/icons/thumb_backward_on.png';

        this.ic_thumb_forward_v = this.settings.ic_thumb_forward_v || 'data/icons/thumb_forward_v.png';
        this.ic_thumb_forward_v_on = this.settings.ic_thumb_forward_v_on || 'data/icons/thumb_forward_v_on.png';
        this.ic_thumb_backward_v = this.settings.ic_thumb_backward_v || 'data/icons/thumb_backward_v.png';
        this.ic_thumb_backward_v_on = this.settings.ic_thumb_backward_v_on || 'data/icons/thumb_backward_v_on.png';

        this._downEvent = "";
        this._moveEvent = "";
        this._upEvent = "";
        this.hasTouch;
        this.touchOn = true;//dont allow touch if less thumbs than fit (no advance buttons)
        if ("ontouchstart" in window) {
            this.hasTouch = true;
            this._downEvent = "touchstart.ap";
            this._moveEvent = "touchmove.ap";
            this._upEvent = "touchend.ap";
        } else {
            this.hasTouch = false;
            this._downEvent = "mousedown.ap";
            this._moveEvent = "mousemove.ap";
            this._upEvent = "mouseup.ap";
        }

        //vars
        this._body = $('body');
        this._window = $(window);
        this._doc = $(document);
        this._windowResizeTimeout = 150;//execute resize delay
        this._windowResizeTimeoutID;
        this._thumbHolderArr = [];
        this._thumbsScrollValue = 100;//horizontal mouse wheel scroll value
        this.thumbTransitionOn = false;
        this.boxWidth;
        this.boxHeight;

        this.thumbContainerWidth;//
        this.thumbContainerHeight;
        this.thumbContainerLeft;
        this.thumbContainerTop;
        this.thumbInnerContainerSize = 0;

        this.gridArr = [];
        this.rows;
        this.columns;
        this.allColumns;
        this.allRows;
        //mouse wheel
        this.columnCounter = 0;
        this.rowCounter = 0;
        this.lastWheelCounter = 0;
        this.scrollPaneApi;

        this.tempScrollOffset;

        this.innerSlideshowExist = false;
        this.slideShowData = [];
        //console.log(this.innerSlideshowDelay);

        //settings
        this.innerSlideshowDelay = this.settings.innerSlideshowDelay;
        this.autoPlay = this.settings.autoPlay;
        this._thumbOrientation = this.settings.thumbOrientation;
        this.buttonSpacing = this.settings.buttonSpacing;
        this._layoutType = this.settings.layoutType;
        this._moveType = this.settings.moveType;
        this.horizontalSpacing = this.settings.horizontalSpacing;
        this.verticalSpacing = this.settings.verticalSpacing;
        this.grid_direction = this.settings.direction;

        this.scrollOffset = this.settings.scrollOffset;

        //dom elements
        this.componentWrapper = $(element);
        this.thumbContainer = this.componentWrapper.find('.thumbContainer');
        this.thumbBackward = this.componentWrapper.find('.thumbBackward').css({ cursor: 'pointer', display: 'none' });
        this.thumbForward = this.componentWrapper.find('.thumbForward').css({ cursor: 'pointer', display: 'none' });
        this.thumbInnerContainer = this.componentWrapper.find('.thumbInnerContainer');

        if (this._moveType != 'scroll') {

            if (!this.isMobile) {
                //buttons hover
                this.thumbForward.bind('mouseover', function () {
                    $(this).find('img').attr('src', self._thumbOrientation == 'horizontal' ? self.ic_thumb_forward_on : self.ic_thumb_forward_v_on);
                    return false;
                }).bind('mouseout', function () {
                    $(this).find('img').attr('src', self._thumbOrientation == 'horizontal' ? self.ic_thumb_forward : self.ic_thumb_forward_v);
                    return false;
                });
                this.thumbBackward.bind('mouseover', function () {
                    $(this).find('img').attr('src', self._thumbOrientation == 'horizontal' ? self.ic_thumb_backward_on : self.ic_thumb_backward_v_on);
                    return false;
                }).bind('mouseout', function () {
                    $(this).find('img').attr('src', self._thumbOrientation == 'horizontal' ? self.ic_thumb_backward : self.ic_thumb_backward_v);
                    return false;
                });
            }

            //buttons click
            if (this._layoutType == 'grid') {

                this.thumbBackward.bind(this._downEvent, function () {
                    if (self.thumbTransitionOn) return;
                    self.thumbTransitionOn = true;
                    var value, thumbContainerSize, num;

                    if (self._thumbOrientation == 'horizontal') {
                        value = parseInt(self.thumbInnerContainer.css('left'), 10);
                        value += self.thumbContainerWidth + self.verticalSpacing;
                        if (value > 0) value = 0;

                        num = Math.ceil(self.thumbContainerWidth / (self.boxWidth + self.verticalSpacing));
                        self.lastWheelCounter += num;

                        self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    } else {
                        value = parseInt(self.thumbInnerContainer.css('top'), 10);
                        value += self.thumbContainerHeight + self.horizontalSpacing;
                        if (value > 0) value = 0;

                        num = Math.ceil(self.thumbContainerHeight / (self.boxHeight + self.horizontalSpacing));
                        self.lastWheelCounter += num;

                        self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    }
                    return false;
                });
                this.thumbForward.bind(this._downEvent, function () {
                    if (self.thumbTransitionOn) return;
                    self.thumbTransitionOn = true;
                    var value, num;

                    if (self._thumbOrientation == 'horizontal') {
                        value = parseInt(self.thumbInnerContainer.css('left'), 10);
                        value -= self.thumbContainerWidth + self.verticalSpacing;
                        if (value < -self.thumbInnerContainerSize + self.thumbContainerWidth) value = -self.thumbInnerContainerSize + self.thumbContainerWidth;

                        num = Math.ceil(self.thumbContainerWidth / (self.boxWidth + self.verticalSpacing));
                        self.lastWheelCounter -= num;

                        self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    } else {//VERTICAL
                        value = parseInt(self.thumbInnerContainer.css('top'), 10);
                        value -= self.thumbContainerHeight + self.horizontalSpacing;
                        if (value < -self.thumbInnerContainerSize + self.thumbContainerHeight) value = -self.thumbInnerContainerSize + self.thumbContainerHeight;

                        num = Math.ceil(self.thumbContainerHeight / (self.boxHeight + self.horizontalSpacing));
                        self.lastWheelCounter -= num;

                        self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    }
                    return false;
                });

            } else {//LINE

                this.thumbBackward.bind(this._downEvent, function () {
                    if (self.thumbTransitionOn) return;
                    self.thumbTransitionOn = true;
                    var value, thumbInnerContainerSize, thumbContainerSize, num;
                    if (self._thumbOrientation == 'horizontal') {

                        thumbInnerContainerSize = self.thumbInnerContainer.width(), thumbContainerSize = self.thumbContainer.width();
                        value = parseInt(self.thumbInnerContainer.css('left'), 10);
                        num = Math.floor(thumbContainerSize / (self.boxWidth + self.spacing));
                        value += num * (self.boxWidth + self.spacing);
                        self.lastWheelCounter += num;
                        if (value > 0) value = 0;

                        if (value % (self.boxWidth + self.spacing) != 0) {//stop on boundary
                            value2 = Math.floor(value / (self.boxWidth + self.spacing));
                            value = value2 * (self.boxWidth + self.spacing);
                            if (value > 0) value = 0;
                            if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;
                        }

                        self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    } else {

                        thumbInnerContainerSize = self.thumbInnerContainer.height(), thumbContainerSize = self.thumbContainer.height();
                        value = parseInt(self.thumbInnerContainer.css('top'), 10);
                        num = Math.floor(thumbContainerSize / (self.boxHeight + self.spacing));
                        value += num * (self.boxHeight + self.spacing);
                        self.lastWheelCounter += num;
                        if (value > 0) value = 0;

                        if (value % (self.boxHeight + self.spacing) != 0) {//stop on boundary
                            value2 = Math.floor(value / (self.boxHeight + self.spacing));
                            value = value2 * (self.boxHeight + self.spacing);
                            if (value > 0) value = 0;
                            if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;
                        }

                        self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    }
                    return false;
                });
                this.thumbForward.bind(this._downEvent, function () {
                    if (self.thumbTransitionOn) return;
                    self.thumbTransitionOn = true;
                    var value, thumbInnerContainerSize, thumbContainerSize, num;
                    if (self._thumbOrientation == 'horizontal') {

                        thumbInnerContainerSize = self.thumbInnerContainer.width(), thumbContainerSize = self.thumbContainer.width();
                        value = parseInt(self.thumbInnerContainer.css('left'), 10);
                        num = Math.floor(thumbContainerSize / (self.boxWidth + self.spacing));
                        value -= num * (self.boxWidth + self.spacing);
                        self.lastWheelCounter -= num;
                        if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;

                        self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });

                    } else {//VERTICAL

                        thumbInnerContainerSize = self.thumbInnerContainer.height(), thumbContainerSize = self.thumbContainer.height();
                        value = parseInt(self.thumbInnerContainer.css('top'), 10);
                        num = Math.floor(thumbContainerSize / (self.boxHeight + self.spacing));
                        value -= num * (self.boxHeight + self.spacing);
                        self.lastWheelCounter -= num;
                        if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;

                        self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                            duration: 700, easing: 'easeOutQuart', complete: function () {
                                self.thumbTransitionOn = false;
                            }
                        });
                    }
                    return false;
                });
            }

            if (!this.isMobile) {
                this.thumbContainer.bind('mousewheel', function (event, delta, deltaX, deltaY) {//mouse wheel
                    if (!self._componentInited) return;
                    if (self.thumbForward.is(':visible') === false)
                        return;
                    
                    self.thumbTransitionOn = true;
                    var d = delta > 0 ? 1 : -1, value, value2, thumbInnerContainerSize, thumbContainerSize;
                    //console.log(d);
                    if (self._layoutType == 'grid') {
                        if (self._thumbOrientation == 'horizontal') {
                            if (self.thumbInnerContainerSize == self.thumbContainerWidth) return;//if same size
                            if (self.columnCounter != self.lastWheelCounter) self.columnCounter = self.lastWheelCounter;//restore last columnCounter if buttons were used meanwhile
                            self.columnCounter += d;
                            if (self.columnCounter > 0) self.columnCounter = 0;
                            else if (self.columnCounter < -self.allColumns + self.columns) self.columnCounter = -self.allColumns + self.columns;
                            self.lastWheelCounter = self.columnCounter;//remember lastWheelCounter
                            value = self.columnCounter * (self.boxWidth + self.verticalSpacing);
                            self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                                duration: 700, easing: 'easeOutQuart', complete: function () {
                                    self.thumbTransitionOn = false;
                                }
                            });
                        } else {
                            if (self.thumbInnerContainerSize == self.thumbContainerHeight) return;//if same size
                            if (self.columnCounter != self.lastWheelCounter) self.rowCounter = self.lastWheelCounter;//restore last rowCounter if buttons were used meanwhile
                            self.rowCounter += d;
                            if (self.rowCounter > 0) self.rowCounter = 0;
                            else if (self.rowCounter < -self.allRows + self.rows) self.rowCounter = -self.allRows + self.rows;
                            self.lastWheelCounter = self.rowCounter;//remember lastWheelCounter
                            value = self.rowCounter * (self.boxHeight + self.horizontalSpacing);
                            self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                                duration: 700, easing: 'easeOutQuart', complete: function () {
                                    self.thumbTransitionOn = false;
                                }
                            });
                        }
                    } else {//LINE

                        if (self._thumbOrientation == 'horizontal') {

                            thumbInnerContainerSize = self.thumbInnerContainer.width(), thumbContainerSize = self.thumbContainer.width();
                            if (thumbInnerContainerSize == thumbContainerSize) return;//if same size

                            if (self.columnCounter != self.lastWheelCounter) self.columnCounter = self.lastWheelCounter;//restore last columnCounter if buttons were used meanwhile
                            self.columnCounter += d;
                            if (self.columnCounter > 0) self.columnCounter = 0;
                            else if (self.columnCounter < -self.allColumns + self.columns) self.columnCounter = -self.allColumns + self.columns;
                            self.lastWheelCounter = self.columnCounter;//remember lastWheelCounter
                            value = self.columnCounter * (self.boxWidth + self.spacing);
                            if (value > 0) value = 0;
                            else if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;

                            if (value % (self.boxWidth + self.spacing) != 0) {//stop on boundary
                                value2 = Math.floor(value / (self.boxWidth + self.spacing));
                                value = value2 * (self.boxWidth + self.spacing);
                                if (value > 0) value = 0;
                                if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;
                            }

                            self.thumbInnerContainer.stop().animate({ 'left': value + 'px' }, {
                                duration: 700, easing: 'easeOutQuart', complete: function () {
                                    self.thumbTransitionOn = false;
                                }
                            });

                        } else {//VERTICAL

                            thumbInnerContainerSize = self.thumbInnerContainer.height(), thumbContainerSize = self.thumbContainer.height();

                            if (self.rowCounter != self.lastWheelCounter) self.rowCounter = self.lastWheelCounter;//restore last columnCounter if buttons were used meanwhile
                            self.rowCounter += d;
                            if (self.rowCounter > 0) self.rowCounter = 0;
                            else if (self.rowCounter < -self.allRows + self.rows) self.rowCounter = -self.allRows + self.rows;
                            self.lastWheelCounter = self.rowCounter;//remember lastWheelCounter
                            value = self.rowCounter * (self.boxHeight + self.spacing);
                            if (value > 0) value = 0;
                            else if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;

                            if (value % (self.boxHeight + self.spacing) != 0) {//stop on boundary
                                value2 = Math.floor(value / (self.boxHeight + self.spacing));
                                value = value2 * (self.boxHeight + self.spacing);
                                if (value > 0) value = 0;
                                if (value < -thumbInnerContainerSize + thumbContainerSize) value = -thumbInnerContainerSize + thumbContainerSize;
                            }

                            self.thumbInnerContainer.stop().animate({ 'top': value + 'px' }, {
                                duration: 700, easing: 'easeOutQuart', complete: function () {
                                    self.thumbTransitionOn = false;
                                }
                            });
                        }
                    }
                    return false;
                });
            }

            //get buttons size
            if (this._thumbOrientation == 'horizontal') {
                this._thumbBackwardSize = this.thumbBackward.width();
                this._thumbForwardSize = this.thumbForward.width();
            } else {
                this._thumbBackwardSize = this.thumbBackward.height();
                this._thumbForwardSize = this.thumbForward.height();
            }
        }

        function initTouch() {
            var startX,
				startY,
				touchStartX,
				touchStartY,
				moved,
				moving = false;

            self.thumbInnerContainer.unbind('touchstart.ap touchmove.ap touchend.ap click.ap-touchclick').bind(
				'touchstart.ap',
				function (e) {
				    if (!self._componentInited) return false;
				    if (!self.touchOn) return true;//if touch disabled we want click executed
				    var touch = e.originalEvent.touches[0];
				    startX = self.thumbInnerContainer.position().left;
				    startY = self.thumbInnerContainer.position().top;
				    touchStartX = touch.pageX;
				    touchStartY = touch.pageY;
				    moved = false;
				    moving = true;
				}
			).bind(
				'touchmove.ap',
				function (ev) {
				    if (!moving) {
				        return;
				    }
				    var touchPos = ev.originalEvent.touches[0], value;
				    if (self._thumbOrientation == 'horizontal') {
				        value = startX - touchStartX + touchPos.pageX;
				        if (value > self._thumbBackwardSize) {
				            value = self._thumbBackwardSize;
				        } else if (value < self._getComponentSize('w') - self.thumbInnerContainerSize - self._thumbForwardSize) {
				            value = self._getComponentSize('w') - self.thumbInnerContainerSize - self._thumbForwardSize;
				        }
				        self.thumbInnerContainer.css('left', value + 'px');
				    } else {
				        value = startY - touchStartY + touchPos.pageY;
				        if (value > self._thumbBackwardSize) {
				            value = self._thumbBackwardSize;
				        } else if (value < self._getComponentSize('h') - self.thumbInnerContainerSize - self._thumbForwardSize) {
				            value = self._getComponentSize('h') - self.thumbInnerContainerSize - self._thumbForwardSize;
				        }
				        self.thumbInnerContainer.css('top', value + 'px');
				    }
				    moved = moved || Math.abs(touchStartX - touchPos.pageX) > 5 || Math.abs(touchStartY - touchPos.pageY) > 5;

				    return false;
				}
			).bind(
				'touchend.ap',
				function (e) {
				    moving = false;
				}
			).bind(
				'click.ap-touchclick',
				function (e) {
				    if (moved) {
				        moved = false;
				        return false;
				    }
				}
			);
        }

        //resize	
        if (!this._componentFixedSize) this._window.bind('resize', function () {
            if (!self._componentInited) return false;
            if (self._windowResizeTimeoutID) clearTimeout(self._windowResizeTimeoutID);
            self._windowResizeTimeoutID = setTimeout(function () { self._doneResizing(); }, self._windowResizeTimeout);
            return false;
        });

        //get playlist
        var i = 0, div, innerDiv, sizeSet = false;
        this.thumbInnerContainer.children("div[class=thumbHolder]").each(function () {
            div = $(this).attr({ 'data-id-i': i, 'data-id-j': 0 });
            self._thumbHolderArr.push(div);

            if (!sizeSet) {
                self.boxWidth = self._thumbHolderArr[0].width();
                self.boxHeight = self._thumbHolderArr[0].height();
                sizeSet = true;
            }

            //search for inner slides
            if (div.find("div[class='innerThumbHolder']").length > 0) {
                self.innerSlideshowExist = true;
                //console.log(i, self.innerSlideshowExist);
                var arr = [], j = 0;
                div.find("div[class='innerThumbHolder']").each(function () {
                    innerDiv = $(this).attr({ 'data-id-i': i, 'data-id-j': j });
                    arr.push(innerDiv);

                    //search for title
                    if (innerDiv.attr('data-title') != undefined && !self._isEmpty(innerDiv.attr('data-title'))) {
                        self.createTitle(innerDiv);
                    }

                    if (!self.isMobile) {
                        //hover events
                        innerDiv.bind('mouseenter', function (e) {
                            if (!self._componentInited) return false;
                            if (!e) var e = window.event;
                            if (e.cancelBubble) e.cancelBubble = true;
                            else if (e.stopPropagation) e.stopPropagation();
                            var currentTarget = $(e.currentTarget);
                            if (typeof overThumb !== 'undefined') overThumb(parseInt(currentTarget.attr('data-id-i'), 10), parseInt(currentTarget.attr('data-id-j'), 10));
                            if (currentTarget.data('caption') == undefined) return;
                            var caption = currentTarget.data('caption'), newy = self.boxHeight - parseInt(caption.data('finalHeight'), 10) + 1;
                            caption.stop().animate({ top: newy + 'px' }, { duration: 500, easing: 'easeOutQuint' });
                            return false;
                        }).bind('mouseleave', function (e) {
                            if (!self._componentInited) return false;
                            if (!e) var e = window.event;
                            if (e.cancelBubble) e.cancelBubble = true;
                            else if (e.stopPropagation) e.stopPropagation();
                            var currentTarget = $(e.currentTarget);
                            if (typeof outThumb !== 'undefined') outThumb(parseInt(currentTarget.attr('data-id-i'), 10), parseInt(currentTarget.attr('data-id-j'), 10));
                            if (currentTarget.data('caption') == undefined) return;
                            var caption = currentTarget.data('caption');
                            caption.stop().animate({ top: self.boxHeight + 'px' }, { duration: 500, easing: 'easeOutQuint' });
                            return false;
                        });
                    }

                    //search for pretty photo
                    if (innerDiv.find('a[class=pp_content]').length > 0) {//pretty photo content
                        //attach click to detect pp open
                        innerDiv.find('a[class=pp_content]').bind('click', function () {
                            if (typeof detailActivated !== 'undefined') detailActivated();
                            /*return false;*/
                        });
                    }

                    if (j > 0) {
                        //hide all except first one 
                        innerDiv.css({
                            display: 'none',
                            opacity: 0
                        });
                    }
                    j++;
                });
                div.data({ 'slideArr': arr, 'position': i, 'counter': 0 });//set data
                self.slideShowData[i] = self.createSlideshow(div);//save slideshows

            } else {//NO INNER SLIDES

                //search for title
                if (div.attr('data-title') != undefined && !self._isEmpty(div.attr('data-title'))) {
                    self.createTitle(div);
                }

                if (!self.isMobile) {
                    //hover events
                    div.bind('mouseenter', function (e) {
                        if (!self._componentInited) return false;
                        if (!e) var e = window.event;
                        if (e.cancelBubble) e.cancelBubble = true;
                        else if (e.stopPropagation) e.stopPropagation();
                        var currentTarget = $(e.currentTarget);
                        if (typeof overThumb !== 'undefined') overThumb(parseInt(currentTarget.attr('data-id-i'), 10), parseInt(currentTarget.attr('data-id-j'), 10));
                        if (currentTarget.data('caption') == undefined) return;
                        var caption = currentTarget.data('caption'), newy = self.boxHeight - parseInt(caption.data('finalHeight'), 10) + 1;
                        caption.stop().animate({ top: newy + 'px' }, { duration: 500, easing: 'easeOutQuint' });
                        return false;
                    }).bind('mouseleave', function (e) {
                        if (!self._componentInited) return false;
                        if (!e) var e = window.event;
                        if (e.cancelBubble) e.cancelBubble = true;
                        else if (e.stopPropagation) e.stopPropagation();
                        var currentTarget = $(e.currentTarget);
                        if (typeof outThumb !== 'undefined') outThumb(parseInt(currentTarget.attr('data-id-i'), 10), parseInt(currentTarget.attr('data-id-j'), 10));
                        if (currentTarget.data('caption') == undefined) return;
                        var caption = currentTarget.data('caption');
                        caption.stop().animate({ top: self.boxHeight + 'px' }, { duration: 500, easing: 'easeOutQuint' });
                        return false;
                    });
                }

                //search for pretty photo
                if (div.find('a[class=pp_content]').length > 0) {//pretty photo content
                    //attach click to detect pp open
                    //console.log(i);
                    div.find('a[class=pp_content]').bind('click', function (e) {
                        if (typeof detailActivated !== 'undefined') detailActivated();
                        /*return false;*/
                    });
                }
            }
            i++;
        });

        this._playlistLength = this._thumbHolderArr.length;

        if (this._layoutType == 'line') {
            if (this._thumbOrientation == 'horizontal') {
                this.allColumns = this._playlistLength;
                this.spacing = parseInt(this._thumbHolderArr[this._playlistLength - 1].css('marginRight'), 10);
                this._thumbHolderArr[this._playlistLength - 1].css('marginRight', 0 + 'px');//remove last margin
            } else {//VERTICAL
                this.allRows = this._playlistLength;
                this.spacing = parseInt(this._thumbHolderArr[this._playlistLength - 1].css('marginBottom'), 10);
                this._thumbHolderArr[this._playlistLength - 1].css('marginBottom', 0 + 'px');//remove last margin
            }
            //get thumbInnerContainerSize
            i = 0;//reset
            for (i; i < this._playlistLength; i++) {
                if (this._thumbOrientation == 'horizontal') {
                    this.thumbInnerContainerSize += this._thumbHolderArr[i].outerWidth(true);
                } else {
                    this.thumbInnerContainerSize += this._thumbHolderArr[i].outerHeight(true);
                }
            }

            //set thumbInnerContainerSize (only once on beginning since it doesnt change in 'line')
            if (this._thumbOrientation == 'horizontal') {
                this.thumbInnerContainer.width(this.thumbInnerContainerSize);
            } else {
                this.thumbInnerContainer.height(this.thumbInnerContainerSize);
            }
        }

        if (this._moveType == 'buttons' && this.hasTouch) {
            initTouch();
        }

        this.thumbInnerContainer.css('display', 'block');

        //init all
        this._doneResizing();

        this._componentInited = true;
        if (typeof thumbGallerySetupDone !== 'undefined') thumbGallerySetupDone();

        $('.thumb_hidden').stop().animate({ 'opacity': 1 }, { duration: 500, easing: 'easeOutSine' });//show thumbs

        if (this.innerSlideshowExist && this.settings.innerSlideshowOn) this.toggleInnerslideShow(true);


    } /* ThumbGallery Constructor End */
    /* -------------------------------------ThumbGallery Prototype------------------------------------------------------*/
    ThumbGallery.prototype = {

        // PUBLIC 

        //DEV:add item
        addHorizontalItem: function (div) {
            div.attr('data-id-i', this._playlistLength);
            div.attr('data-id-j', 0);
            this.thumbInnerContainer.children(':last').removeAttr('style');
            div.css('margin-right', '0px');
            //this.spacing = parseInt(this._thumbHolderArr[this._playlistLength - 1].css('marginRight'), 10);

            this.thumbInnerContainer.append(div);
            this._thumbHolderArr.push(div);
            var w = div.width();
            this.thumbInnerContainerSize = this.thumbInnerContainer.width() + w + this.spacing;
            this.thumbInnerContainer.width(this.thumbInnerContainerSize);

            this._playlistLength += 1;
            this.allColumns += 1;
            $('.thumb_hidden', div).stop().animate({ 'opacity': 1 }, { duration: 500, easing: 'easeOutSine' });//show thumbs
            if (this.thumbForward.is(':visible'))
                this.thumbForward.trigger(this._downEvent);
            if (this._layoutType === 'grid')
                this.layoutTypeGrid();
            else
                this.layoutTypeLine();
        },

        /* INNER SLIDESHOWS */
        /* start/stop all inner slideshows */
        toggleInnerslideShow: function (on) {
            /*to do: find start and end of visible thumbs (scroll, wheel, buttons, with 1 second timeout)*/
            if (!this._componentInited || !this.innerSlideshowExist) return;
            var i = 0, len = this.slideShowData.length;
            for (i; i < len; i++) {
                if (this.slideShowData[i] != undefined) {
                    //console.log(i, this.slideShowData[i]);
                    if (on) {
                        this.slideShowData[i].start();
                    } else {
                        this.slideShowData[i].stop();
                    }
                }
            }
        },
        /* start/stop specific inner slideshow */
        toggleInnerslideShowNum: function (i, on) {
            if (!this._componentInited || !this.innerSlideshowExist) return;
            if (this.slideShowData[i] != undefined) {
                if (on) {
                    this.slideShowData[i].start();
                } else {
                    this.slideShowData[i].stop();
                }
            }
        },
        /* return thumbHolder */
        getThumbHolder: function (i) {
            if (!this._componentInited) return;
            if (this._thumbHolderArr[i] != undefined) {
                return this._thumbHolderArr[i];
            }
        },

        //PRIVATE

        createTitle: function (div) {

            var captionHtml, captionDiv, leftCaptionPadding, rightCaptionPadding, topCaptionPadding, bottomCaptionPadding;

            captionHtml = div.attr('data-title');

            captionDiv = $("<div/>").html(captionHtml).addClass('title').appendTo(this.componentWrapper);

            leftCaptionPadding = parseInt(captionDiv.css('paddingLeft'), 10);
            rightCaptionPadding = parseInt(captionDiv.css('paddingRight'), 10);
            topCaptionPadding = parseInt(captionDiv.css('paddingTop'), 10);
            bottomCaptionPadding = parseInt(captionDiv.css('paddingBottom'), 10);
            //console.log(leftCaptionPadding, rightCaptionPadding, topCaptionPadding, bottomCaptionPadding);

            if (!this.isMobile) {
                captionDiv.css('top', this.boxHeight + 'px');
            } else {
                captionDiv.css('top', this.boxHeight - captionDiv.outerHeight() + 'px');
            }

            captionDiv.css('width', this.boxWidth - leftCaptionPadding - rightCaptionPadding + 'px');
            captionDiv.data('finalHeight', captionDiv.outerHeight());
            captionDiv.appendTo(div);
            div.data('caption', captionDiv);

        },

        //inner slideshows
        createSlideshow: function (div) {
            //console.log('createSlideshow');
            var self = this;

            function ap_slideshow(div) {
                this.slideDiv = div;
                this.len = this.slideDiv.data('slideArr').length;
                this.counter = parseInt(this.slideDiv.data('counter'), 10);
                this.delay;
                this.timeoutID;
                this.time = 1000;
                this.ease = 'easeOutSine';
                this.running = false;
            };

            ap_slideshow.prototype = {

                start: function () {
                    var iself = this;
                    this.delay = self._randomMinMax(self.innerSlideshowDelay[0], self.innerSlideshowDelay[1]);
                    //console.log(delay);
                    this.delay *= 1000;//in miliseconds
                    //console.log(delay);
                    if (this.timeoutID) clearTimeout(this.timeoutID);
                    this.timeoutID = setTimeout(function () {
                        iself.next();
                    }, this.delay);
                    this.running = true;
                },

                stop: function () {
                    if (this.timeoutID) clearTimeout(this.timeoutID);
                    this.running = false;
                },

                next: function () {
                    var iself = this;
                    if (this.timeoutID) clearTimeout(this.timeoutID);
                    var currentSlide = this.slideDiv.data('slideArr')[this.counter].stop().animate({ 'opacity': 0 }, {
                        duration: this.time, easing: this.ease, complete: function () {
                            $(this).css('display', 'none');
                        }
                    });
                    this.counter++;
                    if (this.counter > this.len - 1) this.counter = 0;//loop
                    this.slideDiv.data('counter', this.counter);
                    var nextSlide = this.slideDiv.data('slideArr')[this.counter].css({
                        opacity: 0,
                        display: 'block'
                    }).stop().animate({ 'opacity': 1 }, {
                        duration: this.time, easing: this.ease, complete: function () {
                            if (iself.running) iself.start();
                        }
                    });

                }
            }

            return new ap_slideshow(div);
        },

        checkScroll: function () {
            //console.log('checkScroll');
            var self = this;
            if (!this.scrollPaneApi) {
                this.scrollPaneApi = this.thumbContainer.jScrollPane().data().jsp;
                this.thumbContainer.bind('jsp-initialised', function (event, isScrollable) {
                    //console.log('Handle jsp-initialised', this,'isScrollable=', isScrollable);

                    if (!isScrollable) {
                        if (this._thumbOrientation == 'vertical') {
                            //self.scrollPaneApi.scrollToY(0);
                        } else {
                            //self.scrollPaneApi.scrollToX(0);
                            $('.jspPane').css('left', 0 + 'px');//fix
                        }
                    }

                });

                if (this._thumbOrientation == 'vertical') {
                    this.thumbContainer.jScrollPane({
                        verticalDragMinHeight: 80,
                        verticalDragMaxHeight: 100
                    });
                } else {
                    this.thumbContainer.jScrollPane({
                        horizontalDragMinWidth: 80,
                        horizontalDragMaxWidth: 100
                    });
                    this.thumbContainer.bind('mousewheel', function (event, delta, deltaX, deltaY) {
                        if (!self._componentInited || !self.scrollPaneApi) return;
                        var d = delta > 0 ? -1 : 1;//normalize
                        if (self.scrollPaneApi) self.scrollPaneApi.scrollByX(d * self._thumbsScrollValue);
                        return false;
                    });
                }
            } else {
                this.scrollPaneApi.reinitialise();
            }
        },
        toggleThumbBackward: function (dir) {
            if (dir == 'on') {
                this.thumbBackward.css('display', 'block');
            } else {
                this.thumbBackward.css('display', 'none');
            }
        },
        toggleThumbForward: function (dir) {
            if (dir == 'on') {
                this.thumbForward.css('display', 'block');
            } else {
                this.thumbForward.css('display', 'none');
            }
        },
        //*****************
        calculateGrid: function (scroll_offset) {
            //console.log('calculateGrid');
            this.tempScrollOffset = scroll_offset ? parseInt(scroll_offset, 10) : this.scrollOffset;
            //console.log('tempScrollOffset = ', this.tempScrollOffset);

            var tw = this._getComponentSize('w'), th = this._getComponentSize('h'), currentColumns, currentRows, direction = this.grid_direction == 'left2right' ? true : false;

            if (this._thumbOrientation == 'horizontal') {
                if (this._moveType == 'scroll') th -= this.tempScrollOffset;

                this.rows = Math.floor(th / (this.boxHeight + this.horizontalSpacing));///start by rows, then calculate columns
                //CHECK WITHOUT LAST SPACING!!
                if (this.rows * (this.boxHeight + this.horizontalSpacing) + this.boxHeight <= th) {
                    this.rows += 1;//one more row fits!
                }
                //console.log('this.rows = ', this.rows);

                this.columns = Math.floor(tw / (this.boxWidth + this.verticalSpacing));///max columns that fits in layout
                this.allColumns = Math.ceil(this._playlistLength / this.rows);///actual number of columns
                //console.log('this.columns = ', this.columns, ' , this.allColumns = ', this.allColumns);

                if (this.allColumns < this.columns) {//if all columns is less than fit columns
                    currentColumns = this.allColumns;
                } else {
                    currentColumns = this.columns;
                }
                //console.log('currentColumns = ', currentColumns);

                //create grid for all columns
                this.gridArr = this.createGrid(this.allColumns, this.rows, this.boxWidth, this.boxHeight, this.horizontalSpacing, this.verticalSpacing, 0, 0, direction);
                //console.log(this.gridArr);
                if (this.gridArr[0] == undefined) {
                    alert('Improper grid dimesions!');
                    return false;
                }
                this.thumbInnerContainerSize = this.allColumns * this.boxWidth + (this.allColumns - 1) * this.verticalSpacing;
                //console.log('this.thumbInnerContainerSize = ', this.thumbInnerContainerSize);

                this.thumbContainerWidth = currentColumns * this.boxWidth + (currentColumns - 1) * this.verticalSpacing;
                this.thumbContainerHeight = this.rows * this.boxHeight + (this.rows - 1) * this.horizontalSpacing;

            } else {//leave columns
                if (this._moveType == 'scroll') tw -= this.tempScrollOffset;

                this.columns = Math.floor(tw / (this.boxWidth + this.verticalSpacing));///start by columns, then calculate rows
                //CHECK WITHOUT LAST SPACING!!
                if (this.columns * (this.boxWidth + this.verticalSpacing) + this.boxWidth <= tw) {
                    this.columns += 1;//one more column fits!
                }
                //console.log('this.columns = ', this.columns);

                this.rows = Math.floor(th / (this.boxHeight + this.horizontalSpacing));///max rows that fits in layout
                this.allRows = Math.ceil(this._playlistLength / this.columns);///actual number of rows
                //console.log('this.rows = ', this.rows, ' , this.allRows = ', this.allRows);

                if (this.allRows < this.rows) {//if all columns is less than fit columns
                    currentRows = this.allRows;
                } else {
                    currentRows = this.rows;
                }
                //console.log('currentRows = ', currentRows);

                //create grid for all rows
                this.gridArr = this.createGrid(this.columns, this.allRows, this.boxWidth, this.boxHeight, this.horizontalSpacing, this.verticalSpacing, 0, 0, direction);
                //console.log(this.gridArr);
                if (this.gridArr[0] == undefined) {
                    alert('Improper grid dimesions!');
                    return false;
                }
                this.thumbInnerContainerSize = this.allRows * this.boxHeight + (this.allRows - 1) * this.horizontalSpacing;
                //console.log('this.thumbInnerContainerSize = ', this.thumbInnerContainerSize);

                this.thumbContainerWidth = this.columns * this.boxWidth + (this.columns - 1) * this.verticalSpacing;
                this.thumbContainerHeight = currentRows * this.boxHeight + (currentRows - 1) * this.horizontalSpacing;
            }
            //console.log('this.thumbContainerWidth = ', this.thumbContainerWidth, ' , this.thumbContainerHeight =  ', this.thumbContainerHeight);
        },

        layoutTypeGrid: function () {
            //reposition thumbs
            var i = 0, div;
            for (i; i < this._playlistLength; i++) {
                div = $(this._thumbHolderArr[i]).css({
                    left: this.gridArr[i].x + 'px',
                    top: this.gridArr[i].y + 'px'
                });
            }

            var tw = this._getComponentSize('w'), th = this._getComponentSize('h');

            this.thumbContainerLeft = Math.ceil(tw / 2 - this.thumbContainerWidth / 2);
            this.thumbContainerTop = Math.ceil(th / 2 - this.thumbContainerHeight / 2);
            //console.log('this.thumbContainerLeft = ', this.thumbContainerLeft, ' , this.thumbContainerTop= ',this.thumbContainerTop);

            if (this._moveType != 'scroll') {

                if (this._thumbOrientation == 'horizontal') {

                    //right restrain for rows/columns change 
                    var value = parseInt(this.thumbInnerContainer.css('left'), 10);
                    if (value < -this.thumbInnerContainerSize + this.thumbContainerWidth) {
                        value = -this.thumbInnerContainerSize + this.thumbContainerWidth;
                        this.thumbInnerContainer.css('left', value + 'px');
                    }

                    if (this.thumbInnerContainerSize > this.thumbContainerWidth) {
                        this.thumbBackward.css('display', 'block');
                        this.thumbForward.css('display', 'block');
                        this.touchOn = true;
                    } else {
                        //center thumbs if less
                        this.thumbBackward.css('display', 'none');
                        this.thumbForward.css('display', 'none');
                        this.thumbInnerContainer.css('left', 0 + 'px');
                        this.touchOn = false;
                    }

                    //align buttons
                    //tbl = thumb backward left, tfr = thumb forward right
                    var tbl = this.thumbContainerLeft - this._thumbBackwardSize - this.buttonSpacing;
                    if (tbl < 0) tbl = 0;//restrain
                    var tfr = this.thumbContainerLeft + this.thumbContainerWidth + this.buttonSpacing;
                    if (tfr > tw - this._thumbForwardSize) tfr = tw - this._thumbForwardSize;//restrain
                    this.thumbBackward.css('left', tbl + 'px');
                    this.thumbForward.css('left', tfr + 'px');
                    //console.log('tbl = ', tbl, ' , tfr = ', tfr);

                } else {//VERTICAL

                    var value = parseInt(this.thumbInnerContainer.css('top'), 10);
                    if (value < -this.thumbInnerContainerSize + this.thumbContainerHeight) {
                        value = -this.thumbInnerContainerSize + this.thumbContainerHeight;
                        this.thumbInnerContainer.css('top', value + 'px');
                    }

                    if (this.thumbInnerContainerSize > this.thumbContainerHeight) {
                        this.thumbBackward.css('display', 'block');
                        this.thumbForward.css('display', 'block');
                        this.touchOn = true;
                    } else {
                        this.thumbBackward.css('display', 'none');
                        this.thumbForward.css('display', 'none');
                        this.thumbInnerContainer.css('top', 0 + 'px');
                        this.touchOn = false;
                    }

                    //align buttons
                    //tbt = thumb backward top, tfb = thumb forward bottom
                    var tbt = this.thumbContainerTop - this._thumbBackwardSize - this.buttonSpacing;
                    if (tbt < 0) tbt = 0;//restrain
                    var tfb = this.thumbContainerTop + this.thumbContainerHeight + this.buttonSpacing;
                    if (tfb > th - this._thumbForwardSize) tfb = th - this._thumbForwardSize;//restrain
                    this.thumbBackward.css('top', tbt + 'px');
                    this.thumbForward.css('top', tfb + 'px');

                }

                //align thumbContainer
                this.thumbContainer.css({
                    width: this.thumbContainerWidth + 'px',
                    height: this.thumbContainerHeight + 'px',
                    left: this.thumbContainerLeft + 'px',
                    top: this.thumbContainerTop + 'px'
                });

            } else {//SCROLL

                if (this._thumbOrientation == 'horizontal') {
                    this.thumbContainerTop -= this.tempScrollOffset / 2;
                    this.thumbContainerHeight += this.tempScrollOffset;
                } else {
                    this.thumbContainerLeft -= this.tempScrollOffset / 2;
                    this.thumbContainerWidth += this.tempScrollOffset;
                }

                //align thumbContainer
                this.thumbContainer.css({
                    width: this.thumbContainerWidth + 'px',
                    height: this.thumbContainerHeight + 'px'
                });

                if (this._thumbOrientation == 'horizontal') {
                    this.thumbInnerContainer.css({
                        width: this.thumbInnerContainerSize + 'px',
                        height: this.thumbContainerHeight + 'px'
                    });
                } else {
                    this.thumbInnerContainer.css({
                        width: this.thumbContainerWidth + 'px',
                        height: this.thumbInnerContainerSize + 'px'
                    });
                }
                //console.log(this.thumbContainerWidth, this.thumbContainerHeight, this.thumbContainerLeft, this.thumbContainerTop, this.thumbContainerTop, this.thumbInnerContainerSize);
            }

        },
        layoutTypeLine: function () {
            //console.log('layoutTypeLine');

            var self = this, tw = this._getComponentSize('w'), th = this._getComponentSize('h');

            if (this._thumbOrientation == 'horizontal') {

                if (this._moveType != 'scroll') {

                    var thumbInnerContainerSize = self.thumbInnerContainer.width(), thumbContainerSize = self.thumbContainer.width();
                    //console.log(this.thumbInnerContainerSize, thumbContainerSize);
                    //check buttons and thumbInnerContainer
                    if (this.thumbInnerContainerSize > thumbContainerSize) {
                        this.thumbBackward.css('display', 'block');
                        this.thumbForward.css('display', 'block');
                        this.touchOn = true;

                        //restrain for resize
                        var value = parseInt(this.thumbInnerContainer.css('left'), 10);
                        if (value < thumbContainerSize - thumbInnerContainerSize) {
                            value = thumbContainerSize - thumbInnerContainerSize;
                        } else if (value > 0) {
                            value = 0;
                        }
                        this.thumbInnerContainer.css('left', value + 'px');

                    } else {
                        this.thumbBackward.css('display', 'none');
                        this.thumbForward.css('display', 'none');
                        this.touchOn = false;
                        this.thumbInnerContainer.css('left', thumbContainerSize / 2 - thumbInnerContainerSize / 2 + 'px');//center thumbs if less
                    }

                }
                else {//SCROLL
                    console.log('nothing horizontal');
                }

            }
            else {//VERTICAL

                if (this._moveType != 'scroll') {

                    var thumbInnerContainerSize = self.thumbInnerContainer.height(), thumbContainerSize = self.thumbContainer.height();

                    //check buttons and thumbInnerContainer
                    if (this.thumbInnerContainerSize > thumbContainerSize) {
                        this.thumbBackward.css('display', 'block');
                        this.thumbForward.css('display', 'block');
                        this.touchOn = true;

                        //restrain for resize
                        var value = parseInt(this.thumbInnerContainer.css('top'), 10);
                        if (value < thumbContainerSize - thumbInnerContainerSize) {
                            value = thumbContainerSize - thumbInnerContainerSize;
                        } else if (value > 0) {
                            value = 0;
                        }
                        this.thumbInnerContainer.css('top', value + 'px');

                    } else {
                        this.thumbBackward.css('display', 'none');
                        this.thumbForward.css('display', 'none');
                        this.touchOn = false;
                        this.thumbInnerContainer.css('top', thumbContainerSize / 2 - thumbInnerContainerSize / 2 + 'px');//center thumbs if less
                    }

                }
                else {//scroll
                    console.log('nothing VERTICAL');
                }
            }
        },
        _getComponentSize: function (type) {
            if (type == "w") {//width
                return this.componentWrapper.width();
            } else {//height
                return this.componentWrapper.height();
            }
        },

        /* HELP FUNCTIONS */

        //returns a random value between min and max
        _randomMinMax: function (min, max) {
            return Math.random() * (max - min) + min;
        },

        _stringCounter: function (i) {
            var s;
            if (i < 9) {
                s = "0" + (i + 1);
            } else {
                s = i + 1;
            }
            return s;
        },
        _preventSelect: function (arr) {
            $(arr).each(function () {
                $(this).attr('unselectable', 'on')
					   .css({
					       '-moz-user-select': 'none',
					       '-webkit-user-select': 'none',
					       'user-select': 'none'
					   })
					   .each(function () {
					       this.onselectstart = function () { return false; };
					   });
            });
        },
        _doneResizing: function () {
            //console.log('_doneResizing');
            if (this._layoutType == 'grid') {
                this.calculateGrid(this.scrollOffset);

                if (this._moveType == 'scroll') {
                    if (this._thumbOrientation == 'horizontal') {
                        if (this.thumbInnerContainerSize <= this.thumbContainerWidth) {
                            this.calculateGrid('0');//remove scrollOffset
                        }
                    } else {
                        if (this.thumbInnerContainerSize <= this.thumbContainerHeight) {
                            this.calculateGrid('0');//remove scrollOffset
                        }
                    }
                }

                this.layoutTypeGrid(this.scrollOffset);
            } else {//LINE
                if (this._thumbOrientation == 'horizontal') {
                    this.columns = this.thumbContainer.width() / (this._thumbHolderArr[0].outerWidth(true));
                } else {//VERTICAL
                    this.rows = this.thumbContainer.height() / (this._thumbHolderArr[0].outerHeight(true));
                }
                this.layoutTypeLine();
            }
            if (this._moveType == 'scroll') {
                this.checkScroll();
            }

        },
        _isEmpty: function (str) {
            return str.replace(/^\s+|\s+$/g, '').length == 0;
        },
        createGrid: function (columns, rows, xSpacing, ySpacing, xPadding, yPadding, xOffset, yOffset, leftToRight) {

            var arr = [], pointObj, row, col, num = (columns * rows);

            for (var i = 0; i < num; i++) {
                pointObj = {};

                if (leftToRight) {
                    row = (i % columns);
                    col = Math.floor((i / columns));

                    pointObj.x = (row * (xSpacing + xPadding)) + xOffset;
                    pointObj.y = (col * (ySpacing + yPadding)) + yOffset;

                } else {
                    row = (i % rows);
                    col = Math.floor((i / rows));

                    pointObj.x = (col * (xSpacing + xPadding)) + xOffset;
                    pointObj.y = (row * (ySpacing + yPadding)) + yOffset;

                }
                arr.push(pointObj);
            }
            return arr;
        }

    }; /* ThumbGallery.prototype end */

    $.fn.thumbGallery = function (options) {
        return this.each(function () {
            var thumbGallery = new ThumbGallery($(this), options);
            $(this).data("thumbGallery", thumbGallery);

            //PUBLIC METHODS
            $.fn.thumbGallery.toggleInnerslideShow = function (on) {
                thumbGallery.toggleInnerslideShow(on);
            }
            $.fn.thumbGallery.toggleInnerslideShowNum = function (num, on) {
                thumbGallery.toggleInnerslideShowNum(num, on);
            }
            $.fn.thumbGallery.getThumbHolder = function (num) {
                return thumbGallery.getThumbHolder(num);
            }

        });
    };

    $.fn.thumbGallery.defaults = {};
    $.fn.thumbGallery.settings = {};

})(jQuery);





