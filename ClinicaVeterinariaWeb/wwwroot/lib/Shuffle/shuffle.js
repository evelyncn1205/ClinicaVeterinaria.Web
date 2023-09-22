
!function (t, e) { "object" == typeof exports && "undefined" != typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define(e) : (t = t || self).Shuffle = e() }(this, function () { "use strict"; function t(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") } function e(t, e) { for (var i = 0; i < e.length; i++) { var n = e[i]; n.enumerable = n.enumerable || !1, n.configurable = !0, "value" in n && (n.writable = !0), Object.defineProperty(t, n.key, n) } } function i(t, i, n) { return i && e(t.prototype, i), n && e(t, n), t } function n(t) { return (n = Object.setPrototypeOf ? Object.getPrototypeOf : function (t) { return t.__proto__ || Object.getPrototypeOf(t) })(t) } function s(t, e) { return (s = Object.setPrototypeOf || function (t, e) { return t.__proto__ = e, t })(t, e) } function o(t, e) { return !e || "object" != typeof e && "function" != typeof e ? function (t) { if (void 0 === t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return t }(t) : e } function r() { } r.prototype = { on: function (t, e, i) { var n = this.e || (this.e = {}); return (n[t] || (n[t] = [])).push({ fn: e, ctx: i }), this }, once: function (t, e, i) { var n = this; function s() { n.off(t, s), e.apply(i, arguments) } return s._ = e, this.on(t, s, i) }, emit: function (t) { for (var e = [].slice.call(arguments, 1), i = ((this.e || (this.e = {}))[t] || []).slice(), n = 0, s = i.length; n < s; n++)i[n].fn.apply(i[n].ctx, e); return this }, off: function (t, e) { var i = this.e || (this.e = {}), n = i[t], s = []; if (n && e) for (var o = 0, r = n.length; o < r; o++)n[o].fn !== e && n[o].fn._ !== e && s.push(n[o]); return s.length ? i[t] = s : delete i[t], this } }; var l = r, a = r; l.TinyEmitter = a; var u = "undefined" != typeof Element ? Element.prototype : {}, h = u.matches || u.matchesSelector || u.webkitMatchesSelector || u.mozMatchesSelector || u.msMatchesSelector || u.oMatchesSelector, f = function (t, e) { if (!t || 1 !== t.nodeType) return !1; if (h) return h.call(t, e); for (var i = t.parentNode.querySelectorAll(e), n = 0; n < i.length; n++)if (i[n] == t) return !0; return !1 }; var c = function (t, e) { var i, n, s, o, r = 0; return function () { i = this, n = arguments; var t = new Date - r; return o || (t >= e ? l() : o = setTimeout(l, e - t)), s }; function l() { o = 0, r = +new Date, s = t.apply(i, n), i = null, n = null } }; function d() { } function m(t) { return parseFloat(t) || 0 } var p = function () { function e(i, n) { t(this, e), this.x = m(i), this.y = m(n) } return i(e, null, [{ key: "equals", value: function (t, e) { return t.x === e.x && t.y === e.y } }]), e }(), v = function () { function e(i, n, s, o, r) { t(this, e), this.id = r, this.left = i, this.top = n, this.width = s, this.height = o } return i(e, null, [{ key: "intersects", value: function (t, e) { return t.left < e.left + e.width && e.left < t.left + t.width && t.top < e.top + e.height && e.top < t.top + t.height } }]), e }(), y = { BASE: "shuffle", SHUFFLE_ITEM: "shuffle-item", VISIBLE: "shuffle-item--visible", HIDDEN: "shuffle-item--hidden" }, g = 0, _ = function () { function e(i) { t(this, e), g += 1, this.id = g, this.element = i, this.isVisible = !0, this.isHidden = !1 } return i(e, [{ key: "show", value: function () { this.isVisible = !0, this.element.classList.remove(y.HIDDEN), this.element.classList.add(y.VISIBLE), this.element.removeAttribute("aria-hidden") } }, { key: "hide", value: function () { this.isVisible = !1, this.element.classList.remove(y.VISIBLE), this.element.classList.add(y.HIDDEN), this.element.setAttribute("aria-hidden", !0) } }, { key: "init", value: function () { this.addClasses([y.SHUFFLE_ITEM, y.VISIBLE]), this.applyCss(e.Css.INITIAL), this.scale = e.Scale.VISIBLE, this.point = new p } }, { key: "addClasses", value: function (t) { var e = this; t.forEach(function (t) { e.element.classList.add(t) }) } }, { key: "removeClasses", value: function (t) { var e = this; t.forEach(function (t) { e.element.classList.remove(t) }) } }, { key: "applyCss", value: function (t) { var e = this; Object.keys(t).forEach(function (i) { e.element.style[i] = t[i] }) } }, { key: "dispose", value: function () { this.removeClasses([y.HIDDEN, y.VISIBLE, y.SHUFFLE_ITEM]), this.element.removeAttribute("style"), this.element = null } }]), e }(); _.Css = { INITIAL: { position: "absolute", top: 0, left: 0, visibility: "visible", willChange: "transform" }, VISIBLE: { before: { opacity: 1, visibility: "visible" }, after: { transitionDelay: "" } }, HIDDEN: { before: { opacity: 0 }, after: { visibility: "hidden", transitionDelay: "" } } }, _.Scale = { VISIBLE: 1, HIDDEN: .001 }; var E = null, I = function () { if (null !== E) return E; var t = document.body || document.documentElement, e = document.createElement("div"); return e.style.cssText = "width:10px;padding:2px;box-sizing:border-box;", t.appendChild(e), E = "10px" === window.getComputedStyle(e, null).width, t.removeChild(e), E }; function b(t, e) { var i = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : window.getComputedStyle(t, null), n = m(i[e]); return I() || "width" !== e ? I() || "height" !== e || (n += m(i.paddingTop) + m(i.paddingBottom) + m(i.borderTopWidth) + m(i.borderBottomWidth)) : n += m(i.paddingLeft) + m(i.paddingRight) + m(i.borderLeftWidth) + m(i.borderRightWidth), n } var S = { reverse: !1, by: null, compare: null, randomize: !1, key: "element" }; function T(t, e) { var i = Object.assign({}, S, e), n = Array.from(t), s = !1; return t.length ? i.randomize ? function (t) { for (var e = t.length; e;) { e -= 1; var i = Math.floor(Math.random() * (e + 1)), n = t[i]; t[i] = t[e], t[e] = n } return t }(t) : ("function" == typeof i.by ? t.sort(function (t, e) { if (s) return 0; var n = i.by(t[i.key]), o = i.by(e[i.key]); return void 0 === n && void 0 === o ? (s = !0, 0) : n < o || "sortFirst" === n || "sortLast" === o ? -1 : n > o || "sortLast" === n || "sortFirst" === o ? 1 : 0 }) : "function" == typeof i.compare && t.sort(i.compare), s ? n : (i.reverse && t.reverse(), t)) : [] } var k = {}, w = "transitionend", C = 0; function L(t) { return !!k[t] && (k[t].element.removeEventListener(w, k[t].listener), k[t] = null, !0) } function D(t, e) { var i = w + (C += 1), n = function (t) { t.currentTarget === t.target && (L(i), e(t)) }; return t.addEventListener(w, n), k[i] = { element: t, listener: n }, i } function z(t) { return Math.max.apply(Math, t) } function M(t, e, i, n) { var s = t / e; return Math.abs(Math.round(s) - s) < n && (s = Math.round(s)), Math.min(Math.ceil(s), i) } function A(t, e, i) { if (1 === e) return t; for (var n = [], s = 0; s <= i - e; s++)n.push(z(t.slice(s, s + e))); return n } function F(t, e) { for (var i, n = (i = t, Math.min.apply(Math, i)), s = 0, o = t.length; s < o; s++)if (t[s] >= n - e && t[s] <= n + e) return s; return 0 } function x(t, e) { var i = {}; t.forEach(function (t) { i[t.top] ? i[t.top].push(t) : i[t.top] = [t] }); var n = [], s = [], o = []; return Object.keys(i).forEach(function (t) { var r = i[t]; s.push(r); var l, a = r[r.length - 1], u = a.left + a.width, h = Math.round((e - u) / 2), f = r, c = !1; if (h > 0) { var d = []; (c = r.every(function (t) { var e = new v(t.left + h, t.top, t.width, t.height, t.id), i = !n.some(function (t) { return v.intersects(e, t) }); return d.push(e), i })) && (f = d) } if (!c && r.some(function (t) { return n.some(function (e) { var i = v.intersects(t, e); return i && (l = e), i }) })) { var m = o.findIndex(function (t) { return t.includes(l) }); o.splice(m, 1, s[m]) } n = n.concat(f), o.push(f) }), [].concat.apply([], o).sort(function (t, e) { return t.id - e.id }).map(function (t) { return new p(t.left, t.top) }) } function O(t) { return Array.from(new Set(t)) } var N = 0, H = function (e) { function r(e) { var i, s = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {}; t(this, r), (i = o(this, n(r).call(this))).options = Object.assign({}, r.options, s), i.options.delimeter && (i.options.delimiter = i.options.delimeter), i.lastSort = {}, i.group = r.ALL_ITEMS, i.lastFilter = r.ALL_ITEMS, i.isEnabled = !0, i.isDestroyed = !1, i.isInitialized = !1, i._transitions = [], i.isTransitioning = !1, i._queue = []; var l = i._getElementOption(e); if (!l) throw new TypeError("Shuffle needs to be initialized with an element."); return i.element = l, i.id = "shuffle_" + N, N += 1, i._init(), i.isInitialized = !0, i } return function (t, e) { if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function"); t.prototype = Object.create(e && e.prototype, { constructor: { value: t, writable: !0, configurable: !0 } }), e && s(t, e) }(r, l), i(r, [{ key: "_init", value: function () { if (this.items = this._getItems(), this.options.sizer = this._getElementOption(this.options.sizer), this.element.classList.add(r.Classes.BASE), this._initItems(this.items), this._onResize = this._getResizeFunction(), window.addEventListener("resize", this._onResize), "complete" !== document.readyState) { var t = this.layout.bind(this); window.addEventListener("load", function e() { window.removeEventListener("load", e), t() }) } var e = window.getComputedStyle(this.element, null), i = r.getSize(this.element).width; this._validateStyles(e), this._setColumns(i), this.filter(this.options.group, this.options.initialSort), this.element.offsetWidth, this.setItemTransitions(this.items), this.element.style.transition = "height ".concat(this.options.speed, "ms ").concat(this.options.easing) } }, { key: "_getResizeFunction", value: function () { var t = this._handleResize.bind(this); return this.options.throttle ? this.options.throttle(t, this.options.throttleTime) : t } }, { key: "_getElementOption", value: function (t) { return "string" == typeof t ? this.element.querySelector(t) : t && t.nodeType && 1 === t.nodeType ? t : t && t.jquery ? t[0] : null } }, { key: "_validateStyles", value: function (t) { "static" === t.position && (this.element.style.position = "relative"), "hidden" !== t.overflow && (this.element.style.overflow = "hidden") } }, { key: "_filter", value: function () { var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : this.lastFilter, e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : this.items, i = this._getFilteredSets(t, e); return this._toggleFilterClasses(i), this.lastFilter = t, "string" == typeof t && (this.group = t), i } }, { key: "_getFilteredSets", value: function (t, e) { var i = this, n = [], s = []; return t === r.ALL_ITEMS ? n = e : e.forEach(function (e) { i._doesPassFilter(t, e.element) ? n.push(e) : s.push(e) }), { visible: n, hidden: s } } }, { key: "_doesPassFilter", value: function (t, e) { if ("function" == typeof t) return t.call(e, e, this); var i = e.getAttribute("data-" + r.FILTER_ATTRIBUTE_KEY), n = this.options.delimiter ? i.split(this.options.delimiter) : JSON.parse(i); function s(t) { return n.includes(t) } return Array.isArray(t) ? this.options.filterMode === r.FilterMode.ANY ? t.some(s) : t.every(s) : n.includes(t) } }, { key: "_toggleFilterClasses", value: function (t) { var e = t.visible, i = t.hidden; e.forEach(function (t) { t.show() }), i.forEach(function (t) { t.hide() }) } }, { key: "_initItems", value: function (t) { t.forEach(function (t) { t.init() }) } }, { key: "_disposeItems", value: function (t) { t.forEach(function (t) { t.dispose() }) } }, { key: "_updateItemCount", value: function () { this.visibleItems = this._getFilteredItems().length } }, { key: "setItemTransitions", value: function (t) { var e = this.options, i = e.speed, n = e.easing, s = this.options.useTransforms ? ["transform"] : ["top", "left"], o = Object.keys(_.Css.HIDDEN.before).map(function (t) { return t.replace(/([A-Z])/g, function (t, e) { return "-".concat(e.toLowerCase()) }) }), r = s.concat(o).join(); t.forEach(function (t) { t.element.style.transitionDuration = i + "ms", t.element.style.transitionTimingFunction = n, t.element.style.transitionProperty = r }) } }, { key: "_getItems", value: function () { var t = this; return Array.from(this.element.children).filter(function (e) { return f(e, t.options.itemSelector) }).map(function (t) { return new _(t) }) } }, { key: "_mergeNewItems", value: function (t) { var e = Array.from(this.element.children); return T(this.items.concat(t), { by: function (t) { return e.indexOf(t) } }) } }, { key: "_getFilteredItems", value: function () { return this.items.filter(function (t) { return t.isVisible }) } }, { key: "_getConcealedItems", value: function () { return this.items.filter(function (t) { return !t.isVisible }) } }, { key: "_getColumnSize", value: function (t, e) { var i; return 0 === (i = "function" == typeof this.options.columnWidth ? this.options.columnWidth(t) : this.options.sizer ? r.getSize(this.options.sizer).width : this.options.columnWidth ? this.options.columnWidth : this.items.length > 0 ? r.getSize(this.items[0].element, !0).width : t) && (i = t), i + e } }, { key: "_getGutterSize", value: function (t) { return "function" == typeof this.options.gutterWidth ? this.options.gutterWidth(t) : this.options.sizer ? b(this.options.sizer, "marginLeft") : this.options.gutterWidth } }, { key: "_setColumns", value: function () { var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : r.getSize(this.element).width, e = this._getGutterSize(t), i = this._getColumnSize(t, e), n = (t + e) / i; Math.abs(Math.round(n) - n) < this.options.columnThreshold && (n = Math.round(n)), this.cols = Math.max(Math.floor(n || 0), 1), this.containerWidth = t, this.colWidth = i } }, { key: "_setContainerSize", value: function () { this.element.style.height = this._getContainerSize() + "px" } }, { key: "_getContainerSize", value: function () { return z(this.positions) } }, { key: "_getStaggerAmount", value: function (t) { return Math.min(t * this.options.staggerAmount, this.options.staggerAmountMax) } }, { key: "_dispatch", value: function (t) { var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {}; this.isDestroyed || (e.shuffle = this, this.emit(t, e)) } }, { key: "_resetCols", value: function () { var t = this.cols; for (this.positions = []; t;)t -= 1, this.positions.push(0) } }, { key: "_layout", value: function (t) { var e = this, i = this._getNextPositions(t), n = 0; t.forEach(function (t, s) { function o() { t.applyCss(_.Css.VISIBLE.after) } if (p.equals(t.point, i[s]) && !t.isHidden) return t.applyCss(_.Css.VISIBLE.before), void o(); t.point = i[s], t.scale = _.Scale.VISIBLE, t.isHidden = !1; var r = e.getStylesForTransition(t, _.Css.VISIBLE.before); r.transitionDelay = e._getStaggerAmount(n) + "ms", e._queue.push({ item: t, styles: r, callback: o }), n += 1 }) } }, { key: "_getNextPositions", value: function (t) { var e = this; if (this.options.isCentered) { var i = t.map(function (t, i) { var n = r.getSize(t.element, !0), s = e._getItemPosition(n); return new v(s.x, s.y, n.width, n.height, i) }); return this.getTransformedPositions(i, this.containerWidth) } return t.map(function (t) { return e._getItemPosition(r.getSize(t.element, !0)) }) } }, { key: "_getItemPosition", value: function (t) { return function (t) { for (var e = t.itemSize, i = t.positions, n = t.gridSize, s = t.total, o = t.threshold, r = t.buffer, l = M(e.width, n, s, o), a = A(i, l, s), u = F(a, r), h = new p(n * u, a[u]), f = a[u] + e.height, c = 0; c < l; c++)i[u + c] = f; return h }({ itemSize: t, positions: this.positions, gridSize: this.colWidth, total: this.cols, threshold: this.options.columnThreshold, buffer: this.options.buffer }) } }, { key: "getTransformedPositions", value: function (t, e) { return x(t, e) } }, { key: "_shrink", value: function () { var t = this, e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : this._getConcealedItems(), i = 0; e.forEach(function (e) { function n() { e.applyCss(_.Css.HIDDEN.after) } if (e.isHidden) return e.applyCss(_.Css.HIDDEN.before), void n(); e.scale = _.Scale.HIDDEN, e.isHidden = !0; var s = t.getStylesForTransition(e, _.Css.HIDDEN.before); s.transitionDelay = t._getStaggerAmount(i) + "ms", t._queue.push({ item: e, styles: s, callback: n }), i += 1 }) } }, { key: "_handleResize", value: function () { this.isEnabled && !this.isDestroyed && this.update() } }, { key: "getStylesForTransition", value: function (t, e) { var i = Object.assign({}, e); if (this.options.useTransforms) { var n = this.options.roundTransforms ? Math.round(t.point.x) : t.point.x, s = this.options.roundTransforms ? Math.round(t.point.y) : t.point.y; i.transform = "translate(".concat(n, "px, ").concat(s, "px) scale(").concat(t.scale, ")") } else i.left = t.point.x + "px", i.top = t.point.y + "px"; return i } }, { key: "_whenTransitionDone", value: function (t, e, i) { var n = D(t, function (t) { e(), i(null, t) }); this._transitions.push(n) } }, { key: "_getTransitionFunction", value: function (t) { var e = this; return function (i) { t.item.applyCss(t.styles), e._whenTransitionDone(t.item.element, t.callback, i) } } }, { key: "_processQueue", value: function () { this.isTransitioning && this._cancelMovement(); var t = this.options.speed > 0, e = this._queue.length > 0; e && t && this.isInitialized ? this._startTransitions(this._queue) : e ? (this._styleImmediately(this._queue), this._dispatch(r.EventType.LAYOUT)) : this._dispatch(r.EventType.LAYOUT), this._queue.length = 0 } }, { key: "_startTransitions", value: function (t) { var e = this; this.isTransitioning = !0, function (t, e, i) { i || ("function" == typeof e ? (i = e, e = null) : i = d); var n = t && t.length; if (!n) return i(null, []); var s = !1, o = new Array(n); function r(t) { return function (e, r) { if (!s) { if (e) return i(e, o), void (s = !0); o[t] = r, --n || i(null, o) } } } t.forEach(e ? function (t, i) { t.call(e, r(i)) } : function (t, e) { t(r(e)) }) }(t.map(function (t) { return e._getTransitionFunction(t) }), this._movementFinished.bind(this)) } }, { key: "_cancelMovement", value: function () { this._transitions.forEach(L), this._transitions.length = 0, this.isTransitioning = !1 } }, { key: "_styleImmediately", value: function (t) { if (t.length) { var e = t.map(function (t) { return t.item.element }); r._skipTransitions(e, function () { t.forEach(function (t) { t.item.applyCss(t.styles), t.callback() }) }) } } }, { key: "_movementFinished", value: function () { this._transitions.length = 0, this.isTransitioning = !1, this._dispatch(r.EventType.LAYOUT) } }, { key: "filter", value: function (t, e) { this.isEnabled && ((!t || t && 0 === t.length) && (t = r.ALL_ITEMS), this._filter(t), this._shrink(), this._updateItemCount(), this.sort(e)) } }, { key: "sort", value: function () { var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : this.lastSort; if (this.isEnabled) { this._resetCols(); var e = T(this._getFilteredItems(), t); this._layout(e), this._processQueue(), this._setContainerSize(), this.lastSort = t } } }, { key: "update", value: function () { var t = arguments.length > 0 && void 0 !== arguments[0] && arguments[0]; this.isEnabled && (t || this._setColumns(), this.sort()) } }, { key: "layout", value: function () { this.update(!0) } }, { key: "add", value: function (t) { var e = this, i = O(t).map(function (t) { return new _(t) }); this._initItems(i), this._resetCols(); var n = T(this._mergeNewItems(i), this.lastSort), s = this._filter(this.lastFilter, n), o = function (t) { return i.includes(t) }, r = function (t) { t.scale = _.Scale.HIDDEN, t.isHidden = !0, t.applyCss(_.Css.HIDDEN.before), t.applyCss(_.Css.HIDDEN.after) }, l = this._getNextPositions(s.visible); s.visible.forEach(function (t, i) { o(t) && (t.point = l[i], r(t), t.applyCss(e.getStylesForTransition(t, {}))) }), s.hidden.forEach(function (t) { o(t) && r(t) }), this.element.offsetWidth, this.setItemTransitions(i), this.items = this._mergeNewItems(i), this.filter(this.lastFilter) } }, { key: "disable", value: function () { this.isEnabled = !1 } }, { key: "enable", value: function () { var t = !(arguments.length > 0 && void 0 !== arguments[0]) || arguments[0]; this.isEnabled = !0, t && this.update() } }, { key: "remove", value: function (t) { var e = this; if (t.length) { var i = O(t), n = i.map(function (t) { return e.getItemByElement(t) }).filter(function (t) { return !!t }); this._toggleFilterClasses({ visible: [], hidden: n }), this._shrink(n), this.sort(), this.items = this.items.filter(function (t) { return !n.includes(t) }), this._updateItemCount(), this.once(r.EventType.LAYOUT, function () { e._disposeItems(n), i.forEach(function (t) { t.parentNode.removeChild(t) }), e._dispatch(r.EventType.REMOVED, { collection: i }) }) } } }, { key: "getItemByElement", value: function (t) { return this.items.find(function (e) { return e.element === t }) } }, { key: "resetItems", value: function () { var t = this; this._disposeItems(this.items), this.isInitialized = !1, this.items = this._getItems(), this._initItems(this.items), this.once(r.EventType.LAYOUT, function () { t.setItemTransitions(t.items), t.isInitialized = !0 }), this.filter(this.lastFilter) } }, { key: "destroy", value: function () { this._cancelMovement(), window.removeEventListener("resize", this._onResize), this.element.classList.remove("shuffle"), this.element.removeAttribute("style"), this._disposeItems(this.items), this.items.length = 0, this._transitions.length = 0, this.options.sizer = null, this.element = null, this.isDestroyed = !0, this.isEnabled = !1 } }], [{ key: "getSize", value: function (t) { var e = arguments.length > 1 && void 0 !== arguments[1] && arguments[1], i = window.getComputedStyle(t, null), n = b(t, "width", i), s = b(t, "height", i); e && (n += b(t, "marginLeft", i) + b(t, "marginRight", i), s += b(t, "marginTop", i) + b(t, "marginBottom", i)); return { width: n, height: s } } }, { key: "_skipTransitions", value: function (t, e) { var i = t.map(function (t) { var e = t.style, i = e.transitionDuration, n = e.transitionDelay; return e.transitionDuration = "0ms", e.transitionDelay = "0ms", { duration: i, delay: n } }); e(), t[0].offsetWidth, t.forEach(function (t, e) { t.style.transitionDuration = i[e].duration, t.style.transitionDelay = i[e].delay }) } }]), r }(); return H.ShuffleItem = _, H.ALL_ITEMS = "all", H.FILTER_ATTRIBUTE_KEY = "groups", H.EventType = { LAYOUT: "shuffle:layout", REMOVED: "shuffle:removed" }, H.Classes = y, H.FilterMode = { ANY: "any", ALL: "all" }, H.options = { group: H.ALL_ITEMS, speed: 250, easing: "cubic-bezier(0.4, 0.0, 0.2, 1)", itemSelector: "*", sizer: null, gutterWidth: 0, columnWidth: 0, delimiter: null, buffer: 0, columnThreshold: .01, initialSort: null, throttle: c, throttleTime: 300, staggerAmount: 15, staggerAmountMax: 150, useTransforms: !0, filterMode: H.FilterMode.ANY, isCentered: !1, roundTransforms: !0 }, H.Point = p, H.Rect = v, H.__sorter = T, H.__getColumnSpan = M, H.__getAvailablePositions = A, H.__getShortColumn = F, H.__getCenteredPositions = x, H });//(function (global, factory) {
//  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
//  typeof define === 'function' && define.amd ? define(factory) :
//  (global = typeof globalThis !== 'undefined' ? globalThis : global || self, global.Shuffle = factory());
//})(this, (function () { 'use strict';

//  var tinyEmitter = {exports: {}};

//  function E() {// Keep this empty so it's easier to inherit from
//    // (via https://github.com/lipsmack from https://github.com/scottcorgan/tiny-emitter/issues/3)
//  }

//  E.prototype = {
//    on: function (name, callback, ctx) {
//      var e = this.e || (this.e = {});
//      (e[name] || (e[name] = [])).push({
//        fn: callback,
//        ctx: ctx
//      });
//      return this;
//    },
//    once: function (name, callback, ctx) {
//      var self = this;

//      function listener() {
//        self.off(name, listener);
//        callback.apply(ctx, arguments);
//      }
//      listener._ = callback;
//      return this.on(name, listener, ctx);
//    },
//    emit: function (name) {
//      var data = [].slice.call(arguments, 1);
//      var evtArr = ((this.e || (this.e = {}))[name] || []).slice();
//      var i = 0;
//      var len = evtArr.length;

//      for (i; i < len; i++) {
//        evtArr[i].fn.apply(evtArr[i].ctx, data);
//      }

//      return this;
//    },
//    off: function (name, callback) {
//      var e = this.e || (this.e = {});
//      var evts = e[name];
//      var liveEvents = [];

//      if (evts && callback) {
//        for (var i = 0, len = evts.length; i < len; i++) {
//          if (evts[i].fn !== callback && evts[i].fn._ !== callback) liveEvents.push(evts[i]);
//        }
//      } // Remove event from queue to prevent memory leak
//      // Suggested by https://github.com/lazd
//      // Ref: https://github.com/scottcorgan/tiny-emitter/commit/c6ebfaa9bc973b33d110a84a307742b7cf94c953#commitcomment-5024910


//      liveEvents.length ? e[name] = liveEvents : delete e[name];
//      return this;
//    }
//  };
//  tinyEmitter.exports = E;
//  tinyEmitter.exports.TinyEmitter = E;

//  var arrayParallel = function parallel(fns, context, callback) {
//    if (!callback) {
//      if (typeof context === 'function') {
//        callback = context;
//        context = null;
//      } else {
//        callback = noop;
//      }
//    }

//    var pending = fns && fns.length;
//    if (!pending) return callback(null, []);
//    var finished = false;
//    var results = new Array(pending);
//    fns.forEach(context ? function (fn, i) {
//      fn.call(context, maybeDone(i));
//    } : function (fn, i) {
//      fn(maybeDone(i));
//    });

//    function maybeDone(i) {
//      return function (err, result) {
//        if (finished) return;

//        if (err) {
//          callback(err, results);
//          finished = true;
//          return;
//        }

//        results[i] = result;
//        if (! --pending) callback(null, results);
//      };
//    }
//  };

//  function noop() {}

//  /**
//   * Always returns a numeric value, given a value. Logic from jQuery's `isNumeric`.
//   * @param {*} value Possibly numeric value.
//   * @return {number} `value` or zero if `value` isn't numeric.
//   */
//  function getNumber(value) {
//    return parseFloat(value) || 0;
//  }

//  class Point {
//    /**
//     * Represents a coordinate pair.
//     * @param {number} [x=0] X.
//     * @param {number} [y=0] Y.
//     */
//    constructor(x, y) {
//      this.x = getNumber(x);
//      this.y = getNumber(y);
//    }
//    /**
//     * Whether two points are equal.
//     * @param {Point} a Point A.
//     * @param {Point} b Point B.
//     * @return {boolean}
//     */


//    static equals(a, b) {
//      return a.x === b.x && a.y === b.y;
//    }

//  }

//  class Rect {
//    /**
//     * Class for representing rectangular regions.
//     * https://github.com/google/closure-library/blob/master/closure/goog/math/rect.js
//     * @param {number} x Left.
//     * @param {number} y Top.
//     * @param {number} w Width.
//     * @param {number} h Height.
//     * @param {number} id Identifier
//     * @constructor
//     */
//    constructor(x, y, w, h, id) {
//      this.id = id;
//      /** @type {number} */

//      this.left = x;
//      /** @type {number} */

//      this.top = y;
//      /** @type {number} */

//      this.width = w;
//      /** @type {number} */

//      this.height = h;
//    }
//    /**
//     * Returns whether two rectangles intersect.
//     * @param {Rect} a A Rectangle.
//     * @param {Rect} b A Rectangle.
//     * @return {boolean} Whether a and b intersect.
//     */


//    static intersects(a, b) {
//      return a.left < b.left + b.width && b.left < a.left + a.width && a.top < b.top + b.height && b.top < a.top + a.height;
//    }

//  }

//  var Classes = {
//    BASE: 'shuffle',
//    SHUFFLE_ITEM: 'shuffle-item',
//    VISIBLE: 'shuffle-item--visible',
//    HIDDEN: 'shuffle-item--hidden'
//  };

//  let id$1 = 0;

//  class ShuffleItem {
//    constructor(element, isRTL) {
//      id$1 += 1;
//      this.id = id$1;
//      this.element = element;
//      /**
//       * Set correct direction of item
//       */

//      this.isRTL = isRTL;
//      /**
//       * Used to separate items for layout and shrink.
//       */

//      this.isVisible = true;
//      /**
//       * Used to determine if a transition will happen. By the time the _layout
//       * and _shrink methods get the ShuffleItem instances, the `isVisible` value
//       * has already been changed by the separation methods, so this property is
//       * needed to know if the item was visible/hidden before the shrink/layout.
//       */

//      this.isHidden = false;
//    }

//    show() {
//      this.isVisible = true;
//      this.element.classList.remove(Classes.HIDDEN);
//      this.element.classList.add(Classes.VISIBLE);
//      this.element.removeAttribute('aria-hidden');
//    }

//    hide() {
//      this.isVisible = false;
//      this.element.classList.remove(Classes.VISIBLE);
//      this.element.classList.add(Classes.HIDDEN);
//      this.element.setAttribute('aria-hidden', true);
//    }

//    init() {
//      this.addClasses([Classes.SHUFFLE_ITEM, Classes.VISIBLE]);
//      this.applyCss(ShuffleItem.Css.INITIAL);
//      this.applyCss(this.isRTL ? ShuffleItem.Css.DIRECTION.rtl : ShuffleItem.Css.DIRECTION.ltr);
//      this.scale = ShuffleItem.Scale.VISIBLE;
//      this.point = new Point();
//    }

//    addClasses(classes) {
//      classes.forEach(className => {
//        this.element.classList.add(className);
//      });
//    }

//    removeClasses(classes) {
//      classes.forEach(className => {
//        this.element.classList.remove(className);
//      });
//    }

//    applyCss(obj) {
//      Object.keys(obj).forEach(key => {
//        this.element.style[key] = obj[key];
//      });
//    }

//    dispose() {
//      this.removeClasses([Classes.HIDDEN, Classes.VISIBLE, Classes.SHUFFLE_ITEM]);
//      this.element.removeAttribute('style');
//      this.element = null;
//    }

//  }

//  ShuffleItem.Css = {
//    INITIAL: {
//      position: 'absolute',
//      top: 0,
//      visibility: 'visible',
//      willChange: 'transform'
//    },
//    DIRECTION: {
//      ltr: {
//        left: 0
//      },
//      rtl: {
//        right: 0
//      }
//    },
//    VISIBLE: {
//      before: {
//        opacity: 1,
//        visibility: 'visible'
//      },
//      after: {
//        transitionDelay: ''
//      }
//    },
//    HIDDEN: {
//      before: {
//        opacity: 0
//      },
//      after: {
//        visibility: 'hidden',
//        transitionDelay: ''
//      }
//    }
//  };
//  ShuffleItem.Scale = {
//    VISIBLE: 1,
//    HIDDEN: 0.001
//  };

//  let value = null;
//  var testComputedSize = (() => {
//    if (value !== null) {
//      return value;
//    }

//    const element = document.body || document.documentElement;
//    const e = document.createElement('div');
//    e.style.cssText = 'width:10px;padding:2px;box-sizing:border-box;';
//    element.appendChild(e);
//    const {
//      width
//    } = window.getComputedStyle(e, null); // Fix for issue #314

//    value = Math.round(getNumber(width)) === 10;
//    element.removeChild(e);
//    return value;
//  });

//  /**
//   * Retrieve the computed style for an element, parsed as a float.
//   * @param {Element} element Element to get style for.
//   * @param {string} style Style property.
//   * @param {CSSStyleDeclaration} [styles] Optionally include clean styles to
//   *     use instead of asking for them again.
//   * @return {number} The parsed computed value or zero if that fails because IE
//   *     will return 'auto' when the element doesn't have margins instead of
//   *     the computed style.
//   */

//  function getNumberStyle(element, style) {
//    let styles = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : window.getComputedStyle(element, null);
//    let value = getNumber(styles[style]); // Support IE<=11 and W3C spec.

//    if (!testComputedSize() && style === 'width') {
//      value += getNumber(styles.paddingLeft) + getNumber(styles.paddingRight) + getNumber(styles.borderLeftWidth) + getNumber(styles.borderRightWidth);
//    } else if (!testComputedSize() && style === 'height') {
//      value += getNumber(styles.paddingTop) + getNumber(styles.paddingBottom) + getNumber(styles.borderTopWidth) + getNumber(styles.borderBottomWidth);
//    }

//    return value;
//  }

//  /**
//   * Fisher-Yates shuffle.
//   * http://stackoverflow.com/a/962890/373422
//   * https://bost.ocks.org/mike/shuffle/
//   * @param {Array} array Array to shuffle.
//   * @return {Array} Randomly sorted array.
//   */
//  function randomize(array) {
//    let n = array.length;

//    while (n) {
//      n -= 1;
//      const i = Math.floor(Math.random() * (n + 1));
//      const temp = array[i];
//      array[i] = array[n];
//      array[n] = temp;
//    }

//    return array;
//  }

//  const defaults = {
//    // Use array.reverse() to reverse the results
//    reverse: false,
//    // Sorting function
//    by: null,
//    // Custom sort function
//    compare: null,
//    // If true, this will skip the sorting and return a randomized order in the array
//    randomize: false,
//    // Determines which property of each item in the array is passed to the
//    // sorting method.
//    key: 'element'
//  };
//  /**
//   * You can return `undefined` from the `by` function to revert to DOM order.
//   * @param {Array<T>} arr Array to sort.
//   * @param {SortOptions} options Sorting options.
//   * @return {Array<T>}
//   */

//  function sorter(arr, options) {
//    const opts = { ...defaults,
//      ...options
//    };
//    const original = Array.from(arr);
//    let revert = false;

//    if (!arr.length) {
//      return [];
//    }

//    if (opts.randomize) {
//      return randomize(arr);
//    } // Sort the elements by the opts.by function.
//    // If we don't have opts.by, default to DOM order


//    if (typeof opts.by === 'function') {
//      arr.sort((a, b) => {
//        // Exit early if we already know we want to revert
//        if (revert) {
//          return 0;
//        }

//        const valA = opts.by(a[opts.key]);
//        const valB = opts.by(b[opts.key]); // If both values are undefined, use the DOM order

//        if (valA === undefined && valB === undefined) {
//          revert = true;
//          return 0;
//        }

//        if (valA < valB || valA === 'sortFirst' || valB === 'sortLast') {
//          return -1;
//        }

//        if (valA > valB || valA === 'sortLast' || valB === 'sortFirst') {
//          return 1;
//        }

//        return 0;
//      });
//    } else if (typeof opts.compare === 'function') {
//      arr.sort(opts.compare);
//    } // Revert to the original array if necessary


//    if (revert) {
//      return original;
//    }

//    if (opts.reverse) {
//      arr.reverse();
//    }

//    return arr;
//  }

//  const transitions = {};
//  const eventName = 'transitionend';
//  let count = 0;

//  function uniqueId() {
//    count += 1;
//    return eventName + count;
//  }

//  function cancelTransitionEnd(id) {
//    if (transitions[id]) {
//      transitions[id].element.removeEventListener(eventName, transitions[id].listener);
//      transitions[id] = null;
//      return true;
//    }

//    return false;
//  }
//  function onTransitionEnd(element, callback) {
//    const id = uniqueId();

//    const listener = evt => {
//      if (evt.currentTarget === evt.target) {
//        cancelTransitionEnd(id);
//        callback(evt);
//      }
//    };

//    element.addEventListener(eventName, listener);
//    transitions[id] = {
//      element,
//      listener
//    };
//    return id;
//  }

//  function arrayMax(array) {
//    return Math.max(...array);
//  }

//  function arrayMin(array) {
//    return Math.min(...array);
//  }

//  /**
//   * Determine the number of columns an items spans.
//   * @param {number} itemWidth Width of the item.
//   * @param {number} columnWidth Width of the column (includes gutter).
//   * @param {number} columns Total number of columns
//   * @param {number} threshold A buffer value for the size of the column to fit.
//   * @return {number}
//   */

//  function getColumnSpan(itemWidth, columnWidth, columns, threshold) {
//    let columnSpan = itemWidth / columnWidth; // If the difference between the rounded column span number and the
//    // calculated column span number is really small, round the number to
//    // make it fit.

//    if (Math.abs(Math.round(columnSpan) - columnSpan) < threshold) {
//      // e.g. columnSpan = 4.0089945390298745
//      columnSpan = Math.round(columnSpan);
//    } // Ensure the column span is not more than the amount of columns in the whole layout.


//    return Math.min(Math.ceil(columnSpan), columns);
//  }
//  /**
//   * Retrieves the column set to use for placement.
//   * @param {number} columnSpan The number of columns this current item spans.
//   * @param {number} columns The total columns in the grid.
//   * @return {Array.<number>} An array of numbers represeting the column set.
//   */

//  function getAvailablePositions(positions, columnSpan, columns) {
//    // The item spans only one column.
//    if (columnSpan === 1) {
//      return positions;
//    } // The item spans more than one column, figure out how many different
//    // places it could fit horizontally.
//    // The group count is the number of places within the positions this block
//    // could fit, ignoring the current positions of items.
//    // Imagine a 2 column brick as the second item in a 4 column grid with
//    // 10px height each. Find the places it would fit:
//    // [20, 10, 10, 0]
//    //  |   |   |
//    //  *   *   *
//    //
//    // Then take the places which fit and get the bigger of the two:
//    // max([20, 10]), max([10, 10]), max([10, 0]) = [20, 10, 10]
//    //
//    // Next, find the first smallest number (the short column).
//    // [20, 10, 10]
//    //      |
//    //      *
//    //
//    // And that's where it should be placed!
//    //
//    // Another example where the second column's item extends past the first:
//    // [10, 20, 10, 0] => [20, 20, 10] => 10


//    const available = []; // For how many possible positions for this item there are.

//    for (let i = 0; i <= columns - columnSpan; i++) {
//      // Find the bigger value for each place it could fit.
//      available.push(arrayMax(positions.slice(i, i + columnSpan)));
//    }

//    return available;
//  }
//  /**
//   * Find index of short column, the first from the left where this item will go.
//   *
//   * @param {Array.<number>} positions The array to search for the smallest number.
//   * @param {number} buffer Optional buffer which is very useful when the height
//   *     is a percentage of the width.
//   * @return {number} Index of the short column.
//   */

//  function getShortColumn(positions, buffer) {
//    const minPosition = arrayMin(positions);

//    for (let i = 0, len = positions.length; i < len; i++) {
//      if (positions[i] >= minPosition - buffer && positions[i] <= minPosition + buffer) {
//        return i;
//      }
//    }

//    return 0;
//  }
//  /**
//   * Determine the location of the next item, based on its size.
//   * @param {Object} itemSize Object with width and height.
//   * @param {Array.<number>} positions Positions of the other current items.
//   * @param {number} gridSize The column width or row height.
//   * @param {number} total The total number of columns or rows.
//   * @param {number} threshold Buffer value for the column to fit.
//   * @param {number} buffer Vertical buffer for the height of items.
//   * @return {Point}
//   */

//  function getItemPosition(_ref) {
//    let {
//      itemSize,
//      positions,
//      gridSize,
//      total,
//      threshold,
//      buffer
//    } = _ref;
//    const span = getColumnSpan(itemSize.width, gridSize, total, threshold);
//    const setY = getAvailablePositions(positions, span, total);
//    const shortColumnIndex = getShortColumn(setY, buffer); // Position the item

//    const point = new Point(gridSize * shortColumnIndex, setY[shortColumnIndex]); // Update the columns array with the new values for each column.
//    // e.g. before the update the columns could be [250, 0, 0, 0] for an item
//    // which spans 2 columns. After it would be [250, itemHeight, itemHeight, 0].

//    const setHeight = setY[shortColumnIndex] + itemSize.height;

//    for (let i = 0; i < span; i++) {
//      positions[shortColumnIndex + i] = setHeight;
//    }

//    return point;
//  }
//  /**
//   * This method attempts to center items. This method could potentially be slow
//   * with a large number of items because it must place items, then check every
//   * previous item to ensure there is no overlap.
//   * @param {Array.<Rect>} itemRects Item data objects.
//   * @param {number} containerWidth Width of the containing element.
//   * @return {Array.<Point>}
//   */

//  function getCenteredPositions(itemRects, containerWidth) {
//    const rowMap = {}; // Populate rows by their offset because items could jump between rows like:
//    // a   c
//    //  bbb

//    itemRects.forEach(itemRect => {
//      if (rowMap[itemRect.top]) {
//        // Push the point to the last row array.
//        rowMap[itemRect.top].push(itemRect);
//      } else {
//        // Start of a new row.
//        rowMap[itemRect.top] = [itemRect];
//      }
//    }); // For each row, find the end of the last item, then calculate
//    // the remaining space by dividing it by 2. Then add that
//    // offset to the x position of each point.

//    let rects = [];
//    const rows = [];
//    const centeredRows = [];
//    Object.keys(rowMap).forEach(key => {
//      const itemRects = rowMap[key];
//      rows.push(itemRects);
//      const lastItem = itemRects[itemRects.length - 1];
//      const end = lastItem.left + lastItem.width;
//      const offset = Math.round((containerWidth - end) / 2);
//      let finalRects = itemRects;
//      let canMove = false;

//      if (offset > 0) {
//        const newRects = [];
//        canMove = itemRects.every(r => {
//          const newRect = new Rect(r.left + offset, r.top, r.width, r.height, r.id); // Check all current rects to make sure none overlap.

//          const noOverlap = !rects.some(r => Rect.intersects(newRect, r));
//          newRects.push(newRect);
//          return noOverlap;
//        }); // If none of the rectangles overlapped, the whole group can be centered.

//        if (canMove) {
//          finalRects = newRects;
//        }
//      } // If the items are not going to be offset, ensure that the original
//      // placement for this row will not overlap previous rows (row-spanning
//      // elements could be in the way).


//      if (!canMove) {
//        let intersectingRect;
//        const hasOverlap = itemRects.some(itemRect => rects.some(r => {
//          const intersects = Rect.intersects(itemRect, r);

//          if (intersects) {
//            intersectingRect = r;
//          }

//          return intersects;
//        })); // If there is any overlap, replace the overlapping row with the original.

//        if (hasOverlap) {
//          const rowIndex = centeredRows.findIndex(items => items.includes(intersectingRect));
//          centeredRows.splice(rowIndex, 1, rows[rowIndex]);
//        }
//      }

//      rects = rects.concat(finalRects);
//      centeredRows.push(finalRects);
//    }); // Reduce array of arrays to a single array of points.
//    // https://stackoverflow.com/a/10865042/373422
//    // Then reset sort back to how the items were passed to this method.
//    // Remove the wrapper object with index, map to a Point.

//    return centeredRows.flat().sort((a, b) => a.id - b.id).map(itemRect => new Point(itemRect.left, itemRect.top));
//  }

//  /**
//   * Hyphenates a javascript style string to a css one. For example:
//   * MozBoxSizing -> -moz-box-sizing.
//   * @param {string} str The string to hyphenate.
//   * @return {string} The hyphenated string.
//   */
//  function hyphenate(str) {
//    return str.replace(/([A-Z])/g, (str, m1) => `-${m1.toLowerCase()}`);
//  }

//  function arrayUnique(x) {
//    return Array.from(new Set(x));
//  } // Used for unique instance variables


//  let id = 0;

//  class Shuffle extends tinyEmitter.exports {
//    /**
//     * Categorize, sort, and filter a responsive grid of items.
//     *
//     * @param {Element} element An element which is the parent container for the grid items.
//     * @param {Object} [options=Shuffle.options] Options object.
//     * @constructor
//     */
//    constructor(element) {
//      let options = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};
//      super();
//      this.options = { ...Shuffle.options,
//        ...options
//      };
//      this.lastSort = {};
//      this.group = Shuffle.ALL_ITEMS;
//      this.lastFilter = Shuffle.ALL_ITEMS;
//      this.isEnabled = true;
//      this.isDestroyed = false;
//      this.isInitialized = false;
//      this._transitions = [];
//      this.isTransitioning = false;
//      this._queue = [];

//      const el = this._getElementOption(element);

//      if (!el) {
//        throw new TypeError('Shuffle needs to be initialized with an element.');
//      }

//      this.element = el;
//      this.id = `shuffle_${id}`;
//      id += 1;

//      this._init();

//      this.isInitialized = true;
//    }

//    _init() {
//      this.items = this._getItems();
//      this.sortedItems = this.items;
//      this.options.sizer = this._getElementOption(this.options.sizer); // Add class and invalidate styles

//      this.element.classList.add(Shuffle.Classes.BASE); // Set initial css for each item

//      this._initItems(this.items); // If the page has not already emitted the `load` event, call layout on load.
//      // This avoids layout issues caused by images and fonts loading after the
//      // instance has been initialized.


//      if (document.readyState !== 'complete') {
//        const layout = this.layout.bind(this);
//        window.addEventListener('load', function onLoad() {
//          window.removeEventListener('load', onLoad);
//          layout();
//        });
//      } // Get container css all in one request. Causes reflow


//      const containerCss = window.getComputedStyle(this.element, null);
//      const containerWidth = Shuffle.getSize(this.element).width; // Add styles to the container if it doesn't have them.

//      this._validateStyles(containerCss); // We already got the container's width above, no need to cause another
//      // reflow getting it again... Calculate the number of columns there will be


//      this._setColumns(containerWidth); // Kick off!


//      this.filter(this.options.group, this.options.initialSort); // Bind resize events

//      this._rafId = null; // This is true for all supported browsers, but just to be safe, avoid throwing
//      // an error if ResizeObserver is not present. You can manually add a window resize
//      // event and call `update()` if ResizeObserver is missing, or use Shuffle v5.

//      if ('ResizeObserver' in window) {
//        this._resizeObserver = new ResizeObserver(this._handleResizeCallback.bind(this));

//        this._resizeObserver.observe(this.element);
//      } // The shuffle items haven't had transitions set on them yet so the user
//      // doesn't see the first layout. Set them now that the first layout is done.
//      // First, however, a synchronous layout must be caused for the previous
//      // styles to be applied without transitions.


//      this.element.offsetWidth; // eslint-disable-line no-unused-expressions

//      this.setItemTransitions(this.items);
//      this.element.style.transition = `height ${this.options.speed}ms ${this.options.easing}`;
//    }
//    /**
//     * Retrieve an element from an option.
//     * @param {string|jQuery|Element} option The option to check.
//     * @return {?Element} The plain element or null.
//     * @private
//     */


//    _getElementOption(option) {
//      // If column width is a string, treat is as a selector and search for the
//      // sizer element within the outermost container
//      if (typeof option === 'string') {
//        return this.element.querySelector(option);
//      } // Check for an element


//      if (option && option.nodeType && option.nodeType === 1) {
//        return option;
//      } // Check for jQuery object


//      if (option && option.jquery) {
//        return option[0];
//      }

//      return null;
//    }
//    /**
//     * Ensures the shuffle container has the css styles it needs applied to it.
//     * @param {Object} styles Key value pairs for position and overflow.
//     * @private
//     */


//    _validateStyles(styles) {
//      // Position cannot be static.
//      if (styles.position === 'static') {
//        this.element.style.position = 'relative';
//      } // Overflow has to be hidden.


//      if (styles.overflow !== 'hidden') {
//        this.element.style.overflow = 'hidden';
//      }
//    }
//    /**
//     * Filter the elements by a category.
//     * @param {string|string[]|function(Element):boolean} [category] Category to
//     *     filter by. If it's given, the last category will be used to filter the items.
//     * @param {Array} [collection] Optionally filter a collection. Defaults to
//     *     all the items.
//     * @return {{visible: ShuffleItem[], hidden: ShuffleItem[]}}
//     * @private
//     */


//    _filter() {
//      let category = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : this.lastFilter;
//      let collection = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : this.items;

//      const set = this._getFilteredSets(category, collection); // Individually add/remove hidden/visible classes


//      this._toggleFilterClasses(set); // Save the last filter in case elements are appended.


//      this.lastFilter = category; // This is saved mainly because providing a filter function (like searching)
//      // will overwrite the `lastFilter` property every time its called.

//      if (typeof category === 'string') {
//        this.group = category;
//      }

//      return set;
//    }
//    /**
//     * Returns an object containing the visible and hidden elements.
//     * @param {string|string[]|function(Element):boolean} category Category or function to filter by.
//     * @param {ShuffleItem[]} items A collection of items to filter.
//     * @return {{visible: ShuffleItem[], hidden: ShuffleItem[]}}
//     * @private
//     */


//    _getFilteredSets(category, items) {
//      let visible = [];
//      const hidden = []; // category === 'all', add visible class to everything

//      if (category === Shuffle.ALL_ITEMS) {
//        visible = items; // Loop through each item and use provided function to determine
//        // whether to hide it or not.
//      } else {
//        items.forEach(item => {
//          if (this._doesPassFilter(category, item.element)) {
//            visible.push(item);
//          } else {
//            hidden.push(item);
//          }
//        });
//      }

//      return {
//        visible,
//        hidden
//      };
//    }
//    /**
//     * Test an item to see if it passes a category.
//     * @param {string|string[]|function():boolean} category Category or function to filter by.
//     * @param {Element} element An element to test.
//     * @return {boolean} Whether it passes the category/filter.
//     * @private
//     */


//    _doesPassFilter(category, element) {
//      if (typeof category === 'function') {
//        return category.call(element, element, this);
//      } // Check each element's data-groups attribute against the given category.


//      const attr = element.dataset[Shuffle.FILTER_ATTRIBUTE_KEY];
//      const keys = this.options.delimiter ? attr.split(this.options.delimiter) : JSON.parse(attr);

//      function testCategory(category) {
//        return keys.includes(category);
//      }

//      if (Array.isArray(category)) {
//        if (this.options.filterMode === Shuffle.FilterMode.ANY) {
//          return category.some(testCategory);
//        }

//        return category.every(testCategory);
//      }

//      return keys.includes(category);
//    }
//    /**
//     * Toggles the visible and hidden class names.
//     * @param {{visible, hidden}} Object with visible and hidden arrays.
//     * @private
//     */


//    _toggleFilterClasses(_ref) {
//      let {
//        visible,
//        hidden
//      } = _ref;
//      visible.forEach(item => {
//        item.show();
//      });
//      hidden.forEach(item => {
//        item.hide();
//      });
//    }
//    /**
//     * Set the initial css for each item
//     * @param {ShuffleItem[]} items Set to initialize.
//     * @private
//     */


//    _initItems(items) {
//      items.forEach(item => {
//        item.init();
//      });
//    }
//    /**
//     * Remove element reference and styles.
//     * @param {ShuffleItem[]} items Set to dispose.
//     * @private
//     */


//    _disposeItems(items) {
//      items.forEach(item => {
//        item.dispose();
//      });
//    }
//    /**
//     * Updates the visible item count.
//     * @private
//     */


//    _updateItemCount() {
//      this.visibleItems = this._getFilteredItems().length;
//    }
//    /**
//     * Sets css transform transition on a group of elements. This is not executed
//     * at the same time as `item.init` so that transitions don't occur upon
//     * initialization of a new Shuffle instance.
//     * @param {ShuffleItem[]} items Shuffle items to set transitions on.
//     * @protected
//     */


//    setItemTransitions(items) {
//      const {
//        speed,
//        easing
//      } = this.options;
//      const positionProps = this.options.useTransforms ? ['transform'] : ['top', 'left']; // Allow users to transtion other properties if they exist in the `before`
//      // css mapping of the shuffle item.

//      const cssProps = Object.keys(ShuffleItem.Css.HIDDEN.before).map(k => hyphenate(k));
//      const properties = positionProps.concat(cssProps).join();
//      items.forEach(item => {
//        item.element.style.transitionDuration = `${speed}ms`;
//        item.element.style.transitionTimingFunction = easing;
//        item.element.style.transitionProperty = properties;
//      });
//    }

//    _getItems() {
//      return Array.from(this.element.children).filter(el => el.matches(this.options.itemSelector)).map(el => new ShuffleItem(el, this.options.isRTL));
//    }
//    /**
//     * Combine the current items array with a new one and sort it by DOM order.
//     * @param {ShuffleItem[]} items Items to track.
//     * @return {ShuffleItem[]}
//     */


//    _mergeNewItems(items) {
//      const children = Array.from(this.element.children);
//      return sorter(this.items.concat(items), {
//        by(element) {
//          return children.indexOf(element);
//        }

//      });
//    }

//    _getFilteredItems() {
//      return this.items.filter(item => item.isVisible);
//    }

//    _getConcealedItems() {
//      return this.items.filter(item => !item.isVisible);
//    }
//    /**
//     * Returns the column size, based on column width and sizer options.
//     * @param {number} containerWidth Size of the parent container.
//     * @param {number} gutterSize Size of the gutters.
//     * @return {number}
//     * @private
//     */


//    _getColumnSize(containerWidth, gutterSize) {
//      let size; // If the columnWidth property is a function, then the grid is fluid

//      if (typeof this.options.columnWidth === 'function') {
//        size = this.options.columnWidth(containerWidth); // columnWidth option isn't a function, are they using a sizing element?
//      } else if (this.options.sizer) {
//        size = Shuffle.getSize(this.options.sizer).width; // if not, how about the explicitly set option?
//      } else if (this.options.columnWidth) {
//        size = this.options.columnWidth; // or use the size of the first item
//      } else if (this.items.length > 0) {
//        size = Shuffle.getSize(this.items[0].element, true).width; // if there's no items, use size of container
//      } else {
//        size = containerWidth;
//      } // Don't let them set a column width of zero.


//      if (size === 0) {
//        size = containerWidth;
//      }

//      return size + gutterSize;
//    }
//    /**
//     * Returns the gutter size, based on gutter width and sizer options.
//     * @param {number} containerWidth Size of the parent container.
//     * @return {number}
//     * @private
//     */


//    _getGutterSize(containerWidth) {
//      let size;

//      if (typeof this.options.gutterWidth === 'function') {
//        size = this.options.gutterWidth(containerWidth);
//      } else if (this.options.sizer) {
//        size = getNumberStyle(this.options.sizer, 'marginLeft');
//      } else {
//        size = this.options.gutterWidth;
//      }

//      return size;
//    }
//    /**
//     * Calculate the number of columns to be used. Gets css if using sizer element.
//     * @param {number} [containerWidth] Optionally specify a container width if
//     *    it's already available.
//     */


//    _setColumns() {
//      let containerWidth = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : Shuffle.getSize(this.element).width;

//      const gutter = this._getGutterSize(containerWidth);

//      const columnWidth = this._getColumnSize(containerWidth, gutter);

//      let calculatedColumns = (containerWidth + gutter) / columnWidth; // Widths given from getStyles are not precise enough...

//      if (Math.abs(Math.round(calculatedColumns) - calculatedColumns) < this.options.columnThreshold) {
//        // e.g. calculatedColumns = 11.998876
//        calculatedColumns = Math.round(calculatedColumns);
//      }

//      this.cols = Math.max(Math.floor(calculatedColumns || 0), 1);
//      this.containerWidth = containerWidth;
//      this.colWidth = columnWidth;
//    }
//    /**
//     * Adjust the height of the grid
//     */


//    _setContainerSize() {
//      this.element.style.height = `${this._getContainerSize()}px`;
//    }
//    /**
//     * Based on the column heights, it returns the biggest one.
//     * @return {number}
//     * @private
//     */


//    _getContainerSize() {
//      return arrayMax(this.positions);
//    }
//    /**
//     * Get the clamped stagger amount.
//     * @param {number} index Index of the item to be staggered.
//     * @return {number}
//     */


//    _getStaggerAmount(index) {
//      return Math.min(index * this.options.staggerAmount, this.options.staggerAmountMax);
//    }
//    /**
//     * Emit an event from this instance.
//     * @param {string} name Event name.
//     * @param {Object} [data={}] Optional object data.
//     */


//    _dispatch(name) {
//      let data = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};

//      if (this.isDestroyed) {
//        return;
//      }

//      data.shuffle = this;
//      this.emit(name, data);
//    }
//    /**
//     * Zeros out the y columns array, which is used to determine item placement.
//     * @private
//     */


//    _resetCols() {
//      let i = this.cols;
//      this.positions = [];

//      while (i) {
//        i -= 1;
//        this.positions.push(0);
//      }
//    }
//    /**
//     * Loops through each item that should be shown and calculates the x, y position.
//     * @param {ShuffleItem[]} items Array of items that will be shown/layed
//     *     out in order in their array.
//     */


//    _layout(items) {
//      const itemPositions = this._getNextPositions(items);

//      let count = 0;
//      items.forEach((item, i) => {
//        function callback() {
//          item.applyCss(ShuffleItem.Css.VISIBLE.after);
//        } // If the item will not change its position, do not add it to the render
//        // queue. Transitions don't fire when setting a property to the same value.


//        if (Point.equals(item.point, itemPositions[i]) && !item.isHidden) {
//          item.applyCss(ShuffleItem.Css.VISIBLE.before);
//          callback();
//          return;
//        }

//        item.point = itemPositions[i];
//        item.scale = ShuffleItem.Scale.VISIBLE;
//        item.isHidden = false; // Clone the object so that the `before` object isn't modified when the
//        // transition delay is added.

//        const styles = this.getStylesForTransition(item, ShuffleItem.Css.VISIBLE.before);
//        styles.transitionDelay = `${this._getStaggerAmount(count)}ms`;

//        this._queue.push({
//          item,
//          styles,
//          callback
//        });

//        count += 1;
//      });
//    }
//    /**
//     * Return an array of Point instances representing the future positions of
//     * each item.
//     * @param {ShuffleItem[]} items Array of sorted shuffle items.
//     * @return {Point[]}
//     * @private
//     */


//    _getNextPositions(items) {
//      // If position data is going to be changed, add the item's size to the
//      // transformer to allow for calculations.
//      if (this.options.isCentered) {
//        const itemsData = items.map((item, i) => {
//          const itemSize = Shuffle.getSize(item.element, true);

//          const point = this._getItemPosition(itemSize);

//          return new Rect(point.x, point.y, itemSize.width, itemSize.height, i);
//        });
//        return this.getTransformedPositions(itemsData, this.containerWidth);
//      } // If no transforms are going to happen, simply return an array of the
//      // future points of each item.


//      return items.map(item => this._getItemPosition(Shuffle.getSize(item.element, true)));
//    }
//    /**
//     * Determine the location of the next item, based on its size.
//     * @param {{width: number, height: number}} itemSize Object with width and height.
//     * @return {Point}
//     * @private
//     */


//    _getItemPosition(itemSize) {
//      return getItemPosition({
//        itemSize,
//        positions: this.positions,
//        gridSize: this.colWidth,
//        total: this.cols,
//        threshold: this.options.columnThreshold,
//        buffer: this.options.buffer
//      });
//    }
//    /**
//     * Mutate positions before they're applied.
//     * @param {Rect[]} itemRects Item data objects.
//     * @param {number} containerWidth Width of the containing element.
//     * @return {Point[]}
//     * @protected
//     */


//    getTransformedPositions(itemRects, containerWidth) {
//      return getCenteredPositions(itemRects, containerWidth);
//    }
//    /**
//     * Hides the elements that don't match our filter.
//     * @param {ShuffleItem[]} collection Collection to shrink.
//     * @private
//     */


//    _shrink() {
//      let collection = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : this._getConcealedItems();
//      let count = 0;
//      collection.forEach(item => {
//        function callback() {
//          item.applyCss(ShuffleItem.Css.HIDDEN.after);
//        } // Continuing would add a transitionend event listener to the element, but
//        // that listener would not execute because the transform and opacity would
//        // stay the same.
//        // The callback is executed here because it is not guaranteed to be called
//        // after the transitionend event because the transitionend could be
//        // canceled if another animation starts.


//        if (item.isHidden) {
//          item.applyCss(ShuffleItem.Css.HIDDEN.before);
//          callback();
//          return;
//        }

//        item.scale = ShuffleItem.Scale.HIDDEN;
//        item.isHidden = true;
//        const styles = this.getStylesForTransition(item, ShuffleItem.Css.HIDDEN.before);
//        styles.transitionDelay = `${this._getStaggerAmount(count)}ms`;

//        this._queue.push({
//          item,
//          styles,
//          callback
//        });

//        count += 1;
//      });
//    }
//    /**
//     * Resize handler.
//     * @param {ResizeObserverEntry[]} entries
//     */


//    _handleResizeCallback(entries) {
//      // If shuffle is disabled, destroyed, don't do anything.
//      // You can still manually force a shuffle update with shuffle.update({ force: true }).
//      if (!this.isEnabled || this.isDestroyed) {
//        return;
//      } // The reason ESLint disables this is because for..of generates a lot of extra
//      // code using Babel, but Shuffle no longer supports browsers that old, so
//      // nothing to worry about.
//      // eslint-disable-next-line no-restricted-syntax


//      for (const entry of entries) {
//        if (Math.round(entry.contentRect.width) !== Math.round(this.containerWidth)) {
//          // If there was already an animation waiting, cancel it.
//          cancelAnimationFrame(this._rafId); // Offload updating the DOM until the browser is ready.

//          this._rafId = requestAnimationFrame(this.update.bind(this));
//        }
//      }
//    }
//    /**
//     * Returns styles which will be applied to the an item for a transition.
//     * @param {ShuffleItem} item Item to get styles for. Should have updated
//     *   scale and point properties.
//     * @param {Object} styleObject Extra styles that will be used in the transition.
//     * @return {!Object} Transforms for transitions, left/top for animate.
//     * @protected
//     */


//    getStylesForTransition(item, styleObject) {
//      // Clone the object to avoid mutating the original.
//      const styles = { ...styleObject
//      };

//      if (this.options.useTransforms) {
//        const sign = this.options.isRTL ? '-' : '';
//        const x = this.options.roundTransforms ? Math.round(item.point.x) : item.point.x;
//        const y = this.options.roundTransforms ? Math.round(item.point.y) : item.point.y;
//        styles.transform = `translate(${sign}${x}px, ${y}px) scale(${item.scale})`;
//      } else {
//        if (this.options.isRTL) {
//          styles.right = `${item.point.x}px`;
//        } else {
//          styles.left = `${item.point.x}px`;
//        }

//        styles.top = `${item.point.y}px`;
//      }

//      return styles;
//    }
//    /**
//     * Listen for the transition end on an element and execute the itemCallback
//     * when it finishes.
//     * @param {Element} element Element to listen on.
//     * @param {function} itemCallback Callback for the item.
//     * @param {function} done Callback to notify `parallel` that this one is done.
//     */


//    _whenTransitionDone(element, itemCallback, done) {
//      const id = onTransitionEnd(element, evt => {
//        itemCallback();
//        done(null, evt);
//      });

//      this._transitions.push(id);
//    }
//    /**
//     * Return a function which will set CSS styles and call the `done` function
//     * when (if) the transition finishes.
//     * @param {Object} opts Transition object.
//     * @return {function} A function to be called with a `done` function.
//     */


//    _getTransitionFunction(opts) {
//      return done => {
//        opts.item.applyCss(opts.styles);

//        this._whenTransitionDone(opts.item.element, opts.callback, done);
//      };
//    }
//    /**
//     * Execute the styles gathered in the style queue. This applies styles to elements,
//     * triggering transitions.
//     * @private
//     */


//    _processQueue() {
//      if (this.isTransitioning) {
//        this._cancelMovement();
//      }

//      const hasSpeed = this.options.speed > 0;
//      const hasQueue = this._queue.length > 0;

//      if (hasQueue && hasSpeed && this.isInitialized) {
//        this._startTransitions(this._queue);
//      } else if (hasQueue) {
//        this._styleImmediately(this._queue);

//        this._dispatch(Shuffle.EventType.LAYOUT); // A call to layout happened, but none of the newly visible items will
//        // change position or the transition duration is zero, which will not trigger
//        // the transitionend event.

//      } else {
//        this._dispatch(Shuffle.EventType.LAYOUT);
//      } // Remove everything in the style queue


//      this._queue.length = 0;
//    }
//    /**
//     * Wait for each transition to finish, the emit the layout event.
//     * @param {Object[]} transitions Array of transition objects.
//     */


//    _startTransitions(transitions) {
//      // Set flag that shuffle is currently in motion.
//      this.isTransitioning = true; // Create an array of functions to be called.

//      const callbacks = transitions.map(obj => this._getTransitionFunction(obj));
//      arrayParallel(callbacks, this._movementFinished.bind(this));
//    }

//    _cancelMovement() {
//      // Remove the transition end event for each listener.
//      this._transitions.forEach(cancelTransitionEnd); // Reset the array.


//      this._transitions.length = 0; // Show it's no longer active.

//      this.isTransitioning = false;
//    }
//    /**
//     * Apply styles without a transition.
//     * @param {Object[]} objects Array of transition objects.
//     * @private
//     */


//    _styleImmediately(objects) {
//      if (objects.length) {
//        const elements = objects.map(obj => obj.item.element);

//        Shuffle._skipTransitions(elements, () => {
//          objects.forEach(obj => {
//            obj.item.applyCss(obj.styles);
//            obj.callback();
//          });
//        });
//      }
//    }

//    _movementFinished() {
//      this._transitions.length = 0;
//      this.isTransitioning = false;

//      this._dispatch(Shuffle.EventType.LAYOUT);
//    }
//    /**
//     * The magic. This is what makes the plugin 'shuffle'
//     * @param {string|string[]|function(Element):boolean} [category] Category to filter by.
//     *     Can be a function, string, or array of strings.
//     * @param {SortOptions} [sortOptions] A sort object which can sort the visible set
//     */


//    filter(category, sortOptions) {
//      if (!this.isEnabled) {
//        return;
//      }

//      if (!category || category && category.length === 0) {
//        category = Shuffle.ALL_ITEMS; // eslint-disable-line no-param-reassign
//      }

//      this._filter(category); // Shrink each hidden item


//      this._shrink(); // How many visible elements?


//      this._updateItemCount(); // Update transforms on visible elements so they will animate to their new positions.


//      this.sort(sortOptions);
//    }
//    /**
//     * Gets the visible elements, sorts them, and passes them to layout.
//     * @param {SortOptions} [sortOptions] The options object to pass to `sorter`.
//     */


//    sort() {
//      let sortOptions = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : this.lastSort;

//      if (!this.isEnabled) {
//        return;
//      }

//      this._resetCols();

//      const items = sorter(this._getFilteredItems(), sortOptions);
//      this.sortedItems = items;

//      this._layout(items); // `_layout` always happens after `_shrink`, so it's safe to process the style
//      // queue here with styles from the shrink method.


//      this._processQueue(); // Adjust the height of the container.


//      this._setContainerSize();

//      this.lastSort = sortOptions;
//    }
//    /**
//     * Reposition everything.
//     * @param {object} options options object
//     * @param {boolean} [options.recalculateSizes=true] Whether to calculate column, gutter, and container widths again.
//     * @param {boolean} [options.force=false] By default, `update` does nothing if the instance is disabled. Setting this
//     *    to true forces the update to happen regardless.
//     */


//    update() {
//      let {
//        recalculateSizes = true,
//        force = false
//      } = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};

//      if (this.isEnabled || force) {
//        if (recalculateSizes) {
//          this._setColumns();
//        } // Layout items


//        this.sort();
//      }
//    }
//    /**
//     * Use this instead of `update()` if you don't need the columns and gutters updated
//     * Maybe an image inside `shuffle` loaded (and now has a height), which means calculations
//     * could be off.
//     */


//    layout() {
//      this.update({
//        recalculateSizes: true
//      });
//    }
//    /**
//     * New items have been appended to shuffle. Mix them in with the current
//     * filter or sort status.
//     * @param {Element[]} newItems Collection of new items.
//     */


//    add(newItems) {
//      const items = arrayUnique(newItems).map(el => new ShuffleItem(el, this.options.isRTL)); // Add classes and set initial positions.

//      this._initItems(items); // Determine which items will go with the current filter.


//      this._resetCols();

//      const allItems = this._mergeNewItems(items);

//      const sortedItems = sorter(allItems, this.lastSort);

//      const allSortedItemsSet = this._filter(this.lastFilter, sortedItems);

//      const isNewItem = item => items.includes(item);

//      const applyHiddenState = item => {
//        item.scale = ShuffleItem.Scale.HIDDEN;
//        item.isHidden = true;
//        item.applyCss(ShuffleItem.Css.HIDDEN.before);
//        item.applyCss(ShuffleItem.Css.HIDDEN.after);
//      }; // Layout all items again so that new items get positions.
//      // Synchonously apply positions.


//      const itemPositions = this._getNextPositions(allSortedItemsSet.visible);

//      allSortedItemsSet.visible.forEach((item, i) => {
//        if (isNewItem(item)) {
//          item.point = itemPositions[i];
//          applyHiddenState(item);
//          item.applyCss(this.getStylesForTransition(item, {}));
//        }
//      });
//      allSortedItemsSet.hidden.forEach(item => {
//        if (isNewItem(item)) {
//          applyHiddenState(item);
//        }
//      }); // Cause layout so that the styles above are applied.

//      this.element.offsetWidth; // eslint-disable-line no-unused-expressions
//      // Add transition to each item.

//      this.setItemTransitions(items); // Update the list of items.

//      this.items = this._mergeNewItems(items); // Update layout/visibility of new and old items.

//      this.filter(this.lastFilter);
//    }
//    /**
//     * Disables shuffle from updating dimensions and layout on resize
//     */


//    disable() {
//      this.isEnabled = false;
//    }
//    /**
//     * Enables shuffle again
//     * @param {boolean} [isUpdateLayout=true] if undefined, shuffle will update columns and gutters
//     */


//    enable() {
//      let isUpdateLayout = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : true;
//      this.isEnabled = true;

//      if (isUpdateLayout) {
//        this.update();
//      }
//    }
//    /**
//     * Remove 1 or more shuffle items.
//     * @param {Element[]} elements An array containing one or more
//     *     elements in shuffle
//     * @return {Shuffle} The shuffle instance.
//     */


//    remove(elements) {
//      if (!elements.length) {
//        return;
//      }

//      const collection = arrayUnique(elements);
//      const oldItems = collection.map(element => this.getItemByElement(element)).filter(item => !!item);

//      const handleLayout = () => {
//        this._disposeItems(oldItems); // Remove the collection in the callback


//        collection.forEach(element => {
//          element.parentNode.removeChild(element);
//        });

//        this._dispatch(Shuffle.EventType.REMOVED, {
//          collection
//        });
//      }; // Hide collection first.


//      this._toggleFilterClasses({
//        visible: [],
//        hidden: oldItems
//      });

//      this._shrink(oldItems);

//      this.sort(); // Update the list of items here because `remove` could be called again
//      // with an item that is in the process of being removed.

//      this.items = this.items.filter(item => !oldItems.includes(item));

//      this._updateItemCount();

//      this.once(Shuffle.EventType.LAYOUT, handleLayout);
//    }
//    /**
//     * Retrieve a shuffle item by its element.
//     * @param {Element} element Element to look for.
//     * @return {?ShuffleItem} A shuffle item or undefined if it's not found.
//     */


//    getItemByElement(element) {
//      return this.items.find(item => item.element === element);
//    }
//    /**
//     * Dump the elements currently stored and reinitialize all child elements which
//     * match the `itemSelector`.
//     */


//    resetItems() {
//      // Remove refs to current items.
//      this._disposeItems(this.items);

//      this.isInitialized = false; // Find new items in the DOM.

//      this.items = this._getItems(); // Set initial styles on the new items.

//      this._initItems(this.items);

//      this.once(Shuffle.EventType.LAYOUT, () => {
//        // Add transition to each item.
//        this.setItemTransitions(this.items);
//        this.isInitialized = true;
//      }); // Lay out all items.

//      this.filter(this.lastFilter);
//    }
//    /**
//     * Destroys shuffle, removes events, styles, and classes
//     */


//    destroy() {
//      this._cancelMovement();

//      if (this._resizeObserver) {
//        this._resizeObserver.unobserve(this.element);

//        this._resizeObserver = null;
//      } // Reset container styles


//      this.element.classList.remove('shuffle');
//      this.element.removeAttribute('style'); // Reset individual item styles

//      this._disposeItems(this.items);

//      this.items.length = 0;
//      this.sortedItems.length = 0;
//      this._transitions.length = 0; // Null DOM references

//      this.options.sizer = null;
//      this.element = null; // Set a flag so if a debounced resize has been triggered,
//      // it can first check if it is actually isDestroyed and not doing anything

//      this.isDestroyed = true;
//      this.isEnabled = false;
//    }
//    /**
//     * Returns the outer width of an element, optionally including its margins.
//     *
//     * There are a few different methods for getting the width of an element, none of
//     * which work perfectly for all Shuffle's use cases.
//     *
//     * 1. getBoundingClientRect() `left` and `right` properties.
//     *   - Accounts for transform scaled elements, making it useless for Shuffle
//     *   elements which have shrunk.
//     * 2. The `offsetWidth` property.
//     *   - This value stays the same regardless of the elements transform property,
//     *   however, it does not return subpixel values.
//     * 3. getComputedStyle()
//     *   - This works great Chrome, Firefox, Safari, but IE<=11 does not include
//     *   padding and border when box-sizing: border-box is set, requiring a feature
//     *   test and extra work to add the padding back for IE and other browsers which
//     *   follow the W3C spec here.
//     *
//     * @param {Element} element The element.
//     * @param {boolean} [includeMargins=false] Whether to include margins.
//     * @return {{width: number, height: number}} The width and height.
//     */


//    static getSize(element) {
//      let includeMargins = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;
//      // Store the styles so that they can be used by others without asking for it again.
//      const styles = window.getComputedStyle(element, null);
//      let width = getNumberStyle(element, 'width', styles);
//      let height = getNumberStyle(element, 'height', styles);

//      if (includeMargins) {
//        const marginLeft = getNumberStyle(element, 'marginLeft', styles);
//        const marginRight = getNumberStyle(element, 'marginRight', styles);
//        const marginTop = getNumberStyle(element, 'marginTop', styles);
//        const marginBottom = getNumberStyle(element, 'marginBottom', styles);
//        width += marginLeft + marginRight;
//        height += marginTop + marginBottom;
//      }

//      return {
//        width,
//        height
//      };
//    }
//    /**
//     * Change a property or execute a function which will not have a transition
//     * @param {Element[]} elements DOM elements that won't be transitioned.
//     * @param {function} callback A function which will be called while transition
//     *     is set to 0ms.
//     * @private
//     */


//    static _skipTransitions(elements, callback) {
//      const zero = '0ms'; // Save current duration and delay.

//      const data = elements.map(element => {
//        const {
//          style
//        } = element;
//        const duration = style.transitionDuration;
//        const delay = style.transitionDelay; // Set the duration to zero so it happens immediately

//        style.transitionDuration = zero;
//        style.transitionDelay = zero;
//        return {
//          duration,
//          delay
//        };
//      });
//      callback(); // Cause forced synchronous layout.

//      elements[0].offsetWidth; // eslint-disable-line no-unused-expressions
//      // Put the duration back

//      elements.forEach((element, i) => {
//        element.style.transitionDuration = data[i].duration;
//        element.style.transitionDelay = data[i].delay;
//      });
//    }

//  }

//  Shuffle.ShuffleItem = ShuffleItem;
//  Shuffle.ALL_ITEMS = 'all';
//  Shuffle.FILTER_ATTRIBUTE_KEY = 'groups';
//  /** @enum {string} */

//  Shuffle.EventType = {
//    LAYOUT: 'shuffle:layout',
//    REMOVED: 'shuffle:removed'
//  };
//  /** @enum {string} */

//  Shuffle.Classes = Classes;
//  /** @enum {string} */

//  Shuffle.FilterMode = {
//    ANY: 'any',
//    ALL: 'all'
//  }; // Overrideable options

//  Shuffle.options = {
//    // Initial filter group.
//    group: Shuffle.ALL_ITEMS,
//    // Transition/animation speed (milliseconds).
//    speed: 250,
//    // CSS easing function to use.
//    easing: 'cubic-bezier(0.4, 0.0, 0.2, 1)',
//    // e.g. '.picture-item'.
//    itemSelector: '*',
//    // Element or selector string. Use an element to determine the size of columns
//    // and gutters.
//    sizer: null,
//    // A static number or function that tells the plugin how wide the gutters
//    // between columns are (in pixels).
//    gutterWidth: 0,
//    // A static number or function that returns a number which tells the plugin
//    // how wide the columns are (in pixels).
//    columnWidth: 0,
//    // If your group is not json, and is comma delimited, you could set delimiter
//    // to ','.
//    delimiter: null,
//    // Useful for percentage based heights when they might not always be exactly
//    // the same (in pixels).
//    buffer: 0,
//    // Reading the width of elements isn't precise enough and can cause columns to
//    // jump between values.
//    columnThreshold: 0.01,
//    // Shuffle can be initialized with a sort object. It is the same object
//    // given to the sort method.
//    initialSort: null,
//    // Transition delay offset for each item in milliseconds.
//    staggerAmount: 15,
//    // Maximum stagger delay in milliseconds.
//    staggerAmountMax: 150,
//    // Whether to use transforms or absolute positioning.
//    useTransforms: true,
//    // Affects using an array with filter. e.g. `filter(['one', 'two'])`. With "any",
//    // the element passes the test if any of its groups are in the array. With "all",
//    // the element only passes if all groups are in the array.
//    // Note, this has no effect if you supply a custom filter function.
//    filterMode: Shuffle.FilterMode.ANY,
//    // Attempt to center grid items in each row.
//    isCentered: false,
//    // Attempt to align grid items to right.
//    isRTL: false,
//    // Whether to round pixel values used in translate(x, y). This usually avoids
//    // blurriness.
//    roundTransforms: true
//  };
//  Shuffle.Point = Point;
//  Shuffle.Rect = Rect; // Expose for testing. Hack at your own risk.

//  Shuffle.__sorter = sorter;
//  Shuffle.__getColumnSpan = getColumnSpan;
//  Shuffle.__getAvailablePositions = getAvailablePositions;
//  Shuffle.__getShortColumn = getShortColumn;
//  Shuffle.__getCenteredPositions = getCenteredPositions;

//  return Shuffle;

//}));
////# sourceMappingURL=shuffle.js.map
