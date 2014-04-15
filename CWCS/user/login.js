Ext.require([
    'Ext.form.*',
    'Ext.tip.QuickTipManager'
]);

Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();

    var formPanel = Ext.widget('form', {
        renderTo: Ext.getBody(),
        frame: true,
        width: 350,
        bodyPadding: 10,
        bodyBorder: true,
        title: '登陆',

        defaults: {
            anchor: '100%'
        },
        fieldDefaults: {
            labelWidth: 110,
            labelAlign: 'left',
            msgTarget: 'none',
            invalidCls: '' //unset the invalidCls so individual fields do not get styled as invalid
        },

        items: [{
            xtype: 'textfield',
            name: 'USER_NAME',
            fieldLabel: '用户名',
            allowBlank: false,
            minLength: 1
        }, {
            xtype: 'textfield',
            name: 'PASSWORD',
            fieldLabel: '密码',
            inputType: 'password',
            style: 'margin-top:15px',
            allowBlank: false,
            minLength: 1
        }],

        dockedItems: [{
            cls: Ext.baseCSSPrefix + 'dd-drop-ok',
            xtype: 'container',
            dock: 'bottom',
            layout: {
                type: 'hbox',
                align: 'middle'
            },
            padding: '10 10 5',

            items: [{
                xtype: 'button',
                formBind: true,
                disabled: true,
                text: '登陆',
                width: 140,
                handler: function () {
                    var form = this.up('form').getForm();

                    if (form.isValid()) {
                        form.submit({
                            clientValidation: true,
                            url: 'login.aspx',
                            success: function (form, action) {
                                //...
                                var response = Ext.decode(action.response.responseText);
                                if (response.success)
                                    window.location = "/frame/frame.htm";
                                else {
                                    Ext.Msg.show({
                                        title: '登陆失败',
                                        msg: response.msg,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.ERROR
                                    });
                                }
                            },
                            failure: function (form, action) {
                                //...
                                //action.result.msg
                                var response = Ext.decode(action.response.responseText);
                                Ext.Msg.show({
                                    title: '登陆失败',
                                    msg: response.msg,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.ERROR
                                });
                            }
                        });
                    }
                }
            }]
        }]
    });

});