// Custom setting CKEditor
CKEDITOR.editorConfig = function( config ) {
    config.toolbar = [
               { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source', '-', 'Maximize' ] },
               { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
               { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', '-', 'CopyFormatting', 'RemoveFormat'] },
               { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'] },
               { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Replace', '-', 'SelectAll', '-'] },
               { name: 'insert', items: ['Image', 'Flash', 'Table', 'Iframe'] },
               { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
               { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
               { name: 'colors', items: ['TextColor', 'BGColor'] }
    ];
    config.extraPlugins = 'wordcount,undo,htmlwriter,notification,toolbar,button,widget,lineutils,widgetselection,image2';
    config.extraAllowedContent = 'span;ul;li;table;td;style;*[id];*(*);*{*}';
    config.image2_alignClasses = ['align-left', 'align-center', 'align-right'];
    config.image2_captionedClass = 'image-captioned';
    config.allowedContent = true;
    config.fillEmptyBlocks = false;
    config.entities = false;
    config.tabSpaces = 0;
    config.basicEntities = false;
    config.removePlugins = 'blockquote';
    config.entities_greek = false;
    config.entities_latin = false;
    config.entities_additional = '';
    config.templates_files = ['/Administration/Style/plugins/ckeditor/plugins/templates/templates/mytemplates.js'];
    config.filebrowserBrowseUrl = '/Administration/Style/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Administration/Style/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Administration/Style/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Administration/Style/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Administration/Style/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Administration/Style/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
