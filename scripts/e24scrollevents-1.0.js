/*
 e24ScrollEvents
	- MooTools version required: 1.2.2
	- MooTools components required: 
		Core: Element.Dimensions,  Element.Style, Element.Events, Class.Extras  and dependencies

	Changelog:
		- 1.0: First release
*/

/* Copyright: equipo24 S.L.N.E <http://equipo24.com/> - Distributed under MIT License - Keep this message! */

var e24ScrollEvents = new Class({
	
	Implements: [Options, Events],

	options: {
		container: window,
		mode: 'vertical',
		elements: []
	},

	initialize: function(options){
		this.setOptions(options);
		this.container = $(this.options.container);
		this.visibles = new Array();
		
		this.size = (this.options.mode == 'horizontal')?this.container.getSize().x:this.container.getSize().y;			
		this.listenScroll();
		
		this.container.addEvent('scroll', function() {
			this.listenScroll();
		}.bind(this));				
		
		this.container.addEvent('resize', function() {
			this.size = (this.options.mode == 'horizontal')?this.container.getSize().x:this.container.getSize().y;			
			this.listenScroll();
		}.bind(this));				
	},
	
	listenScroll: function() {
		this.options.elements.each(function(el) {
			var scrollPos = (this.options.mode == 'horizontal')?this.container.getScroll().x:this.container.getScroll().y;
			var pos = (this.options.mode == 'horizontal')?el.getPosition(this.container).x:el.getPosition(this.container).y;
			var elSize = (this.options.mode == 'horizontal')?el.getSize().x:el.getSize().y;
			
			if (pos >= scrollPos && pos <= scrollPos + this.size || pos + elSize >= scrollPos && pos + elSize <=  scrollPos + this.size) {
				this.visibles.include(el);
				el.fireEvent('visible', [el, pos]);
				this.fireEvent('visible', [el, pos]);
			}
			else {
				if 	(this.visibles.contains(el)) {
					this.visibles.erase(el);
					el.fireEvent('hidden', [el, pos]);
					this.fireEvent('hidden', [el, pos]);
				}	
			}
		}, this);	
	}		
});

