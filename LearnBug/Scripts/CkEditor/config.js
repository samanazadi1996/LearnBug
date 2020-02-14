/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.skin = 'moono_blue';
    config.toolbar = [
        { items: ["Source"] },
        { items: ["Cut", "Copy", "Paste", "PasteFromWord", "-", "Undo", "Redo"] },
        { items: ["Find", "Replace"] },
        { items: ["Link", "Unlink", "-", "Image", "Table", "HorizontalRule"] },
        { items: ["NumberedList", "BulletedList", "-", "Outdent", "Indent"] },
        { items: ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-", "BidiLtr", "BidiRtl"] },
        { items: ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-", "RemoveFormat"] },
        { items: ["Font", "FontSize", "TextColor", "BGColor"] }
    ];
    config.enterMode = CKEDITOR.ENTER_BR;
    config.width = '100%';
};