Ext.require(['*']);
userInfo = AC.Init('FRAME');
if (!userInfo) {
    alert("No permission to view this page.");
    close();
} else
Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    Ext.state.Manager.setProvider(Ext.create('Ext.state.CookieProvider'));
    var treeStore = Ext.create('Ext.data.TreeStore', {
        proxy: {
            type: 'ajax',
            url: '/category/CategoryBaseInfo.aspx'
        }

    });
    var treePanel = Ext.create('Ext.tree.Panel', {
        title: 'CWCS',
        region: 'west',
        stateId: 'navigation-panel',
        id: 'west-panel',
        split: true,
        margins: '0 0 0 5',
        width: 200,
        minWidth: 175,
        maxWidth: 400,
        store: treeStore,
        rootVisible: false
    });
    treePanel.on("itemclick", function (view, record, item, index, e) {
        maincontent.location = '/Default.aspx?id=' + record.raw.id + '&text=' + record.raw.text;
    });
    var viewport = Ext.create('Ext.Viewport', {
        id: 'mainframe',
        layout: 'border',
        items: [
            Ext.create('Ext.Component', {
                region: 'north',
                height: 32,
                autoEl: { tag: 'div',
                    html: '<p>north - generally for menus, toolbars and/or advertisements'+userInfo.NickName+'</p>'
                }
            }),
            Ext.create('Ext.Panel', {
                region: 'center',
                deferredRender: false,
                items: [
                    Ext.create("Ext.ux.IFrame", {
                        id: '../../maincontent',
                        height: '100%',
                        frameName: 'maincontent',
                        src: "/Default.aspx"
                    })
                ]
            }),
            treePanel

        ]
    });
});