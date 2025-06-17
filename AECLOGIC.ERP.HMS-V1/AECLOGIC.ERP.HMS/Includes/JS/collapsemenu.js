// JScript File
// CollapseMenu Object
// a slick collapsable navigation menu object
// 19990606

// Copyright (C) 1999 Dan Steinman
// Distributed under the terms of the GNU Library General Public License
// Available at http://www.dansteinman.com/dynapi/

function CollapseMenu(x,y,width,numBlocks,name) {
	this.name = (name!=null)? name : "CollapseMenu"+(CollapseMenu.count++)
	this.x = x
	this.y = y
	this.w = width
	this.numBlocks = numBlocks
	this.bgColor = '#ffffff'
	this.openStyle = "slide"  // or 'glide' or 'move'
	this.contentIndent = 0
	this.inc = 5
	this.speed = 20
	this.active = false
	this.obj = this.name + "Object"
	eval(this.obj + "=this")
	this.build = CollapseMenuBuild
	this.activate = CollapseMenuActivate
	this.toggle = CollapseMenuToggle
	this.open = CollapseMenuOpen
	this.close = CollapseMenuClose
	this.finish = CollapseMenuFinish
	this.onToggle = new Function()
}
function CollapseMenuBuild() {
	this.css = css(this.name,this.x,this.y,this.w,0)
	for (var i=0;i<this.numBlocks;i++) {
		this.css += css(this.name+'Block'+i,0,0,this.w)
		this.css += css(this.name+'Block'+i+'Item',0,0,this.w,null,this.bgColor)
		this.css += css(this.name+'Block'+i+'Content',0,0,this.w,null,this.bgColor,null,null,'margin-left:'+this.contentIndent)
	}
	this.css += css(this.name+'Block'+this.numBlocks,0,0,this.w,0,this.bgColor)
	this.divStart = '<div id="'+this.name+'">'
	this.divEnd = ''
	this.divEnd += '<div id="'+this.name+'Block'+this.numBlocks+'"></div>'
	for (var i=0;i<this.numBlocks;i++) {
		this.divEnd += '</div>'
	}
	this.divEnd += '</div>'
}
function CollapseMenuActivate() {
	this.lyr = new DynLayer(this.name)
	this.blocks = new Array()
	this.itemTotal = 0
	this.contentTotal = 0
	for (var i=0;i<this.numBlocks;i++) {
		this.blocks[i] = new Object()
		this.blocks[i].open = false
		this.blocks[i].lyr = new DynLayer(this.name+'Block'+i)
		this.blocks[i].itemlyr = new DynLayer(this.name+'Block'+i+'Item')
		this.blocks[i].itemHeight = this.blocks[i].itemlyr.getContentHeight()
		this.itemTotal += this.blocks[i].itemHeight
		this.blocks[i].itemlyr.clipTo(0,this.w,this.blocks[i].itemHeight,0)
		
		this.blocks[i].contentlyr = new DynLayer(this.name+'Block'+i+'Content')
		this.blocks[i].contentHeight = this.blocks[i].contentlyr.getContentHeight()
		this.contentTotal += this.blocks[i].contentHeight
		
		this.blocks[i].contentlyr.clipTo(0,this.w,this.blocks[i].contentHeight,0)
		this.blocks[i].contentlyr.moveTo(null,this.blocks[i].itemHeight)
		if (i!=0) this.blocks[i].lyr.moveTo(null,this.blocks[i-1].itemHeight)
		this.blocks[i].h = this.blocks[i].itemHeight + this.blocks[i].contentHeight
	}
	this.h = this.contentTotal + this.itemTotal
	for (var i=this.numBlocks-1;i>=0;i--) {
		this.blocks[i].lyr.clipInit()
		this.blocks[i].lyr.clipTo(0,this.w,this.h-this.blocks[i].lyr.y,0)
	}
	this.blocks[this.numBlocks] = new Object()
	this.blocks[this.numBlocks].lyr = new DynLayer(this.name+'Block'+this.numBlocks)
	this.blocks[this.numBlocks].lyr.clipTo(0,this.w,this.h-this.itemTotal,0)
	this.blocks[this.numBlocks].lyr.css.height = this.h-this.itemTotal
	this.blocks[this.numBlocks].lyr.moveTo(null,this.blocks[this.numBlocks-1].itemHeight)
	this.lyr.clipTo(0,this.w,this.h,0)
}
function CollapseMenuToggle(i) {
	if (this.active) return
	this.active = true
	if (!this.blocks[i].open) this.open(i)
	else this.close(i)
}
function CollapseMenuOpen(i) {
	if (!this.blocks[i].open) {
		var h = this.blocks[i].contentHeight + this.blocks[i].itemHeight
		this.blocks[i].open = true
		if (this.openStyle == 'slide') this.blocks[i+1].lyr.slideTo(null,h,this.inc,this.speed,this.obj+'.finish()')
		else if (this.openStyle == 'glide') this.blocks[i+1].lyr.glideTo('slow','slow',null,h,this.inc,this.speed,this.obj+'.finish()')
		else if (this.openStyle == 'move') {this.blocks[i+1].lyr.moveTo(null,h); this.finish();}
	}
}
function CollapseMenuClose(i) {
	if (this.blocks[i].open) {
		var h = this.blocks[i].itemHeight
		this.blocks[i].open = false
		if (this.openStyle == 'slide') this.blocks[i+1].lyr.slideTo(null,h,this.inc,this.speed,this.obj+'.finish()')
		else if (this.openStyle == 'glide') this.blocks[i+1].lyr.glideTo('slow','slow',null,h,this.inc,this.speed,this.obj+'.finish()')
		else if (this.openStyle == 'move') {this.blocks[i+1].lyr.moveTo(null,h); this.finish();}
	}
}
function CollapseMenuFinish() {
	this.active = false
	this.onToggle()
}
CollapseMenu.count = 0
i r
