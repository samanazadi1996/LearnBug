/*
 * Base64Image Plugin for CKEditor (http://github.com/nmmf/base64image)
 * Created by ALL-INKL.COM - Neue Medien Münnich - 04. Feb 2014
 * Licensed under the terms of GPL, LGPL and MPL licenses.
 */
CKEDITOR.plugins.add("base64image", {
	requires: 	"dialog",
	icons	:	"base64image",
	hidpi	:	true,
    init	: 	function(editor){
					editor.addCommand("base64imageDialog", new CKEDITOR.dialogCommand("base64imageDialog"));
					editor.ui.addButton("base64image", {
						label: editor.lang.common.image,
						command: "base64imageDialog",
						toolbar: "insert"
					});
					CKEDITOR.dialog.add("base64imageDialog", this.path+"dialogs/base64image.js");
					editor.on("doubleclick", function(evt){
						if(evt.data.element && !evt.data.element.isReadOnly() && evt.data.element.getName() === "img") {
							evt.data.dialog = "base64imageDialog";
							editor.getSelection().selectElement(evt.data.element);
						}
					});
					if(editor.addMenuItem) {
						editor.addMenuGroup("base64imageGroup");
						editor.addMenuItem("base64imageItem", {
							label: editor.lang.common.image,
							icon: this.path+"icons/base64image.png",
							command: "base64imageDialog",
							group: "base64imageGroup"
						});
					}
					if(editor.contextMenu) {
						editor.contextMenu.addListener(function(element, selection) {
							if(element && element.getName() === "img") {
								editor.getSelection().selectElement(element);
								return { base64imageItem: CKEDITOR.TRISTATE_ON };
							}
							return null;
						});
					}
				}
});

