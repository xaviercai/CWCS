Ext.ns("AC");
AC.SyncCall = function(url) {
    function createXhrObject() {
        var http;
        var activeX = ['MSXML2.XMLHTTP.3.0', 'MSXML2.XMLHTTP', 'Microsoft.XMLHTTP'];

        try {
            http = new XMLHttpRequest();
        } catch (e) {
            for (var i = 0; i < activeX.length; ++i) {
                try {
                    http = new ActiveXObject(activeX[i]);
                    break;
                } catch (e) { }
            }
        } finally {
            return http;
        }
    };

    var conn = createXhrObject();
    conn.open("GET", url, false);
    conn.send(null);
    if (conn.responseText != '') {
        return Ext.decode(conn.responseText);
    } else {
        return null;
    }
};
AC.Init = function(table) {
    return AC.SyncCall('/user/UserInfo.aspx?table=' + table);
}
AC.Logout = function() {
    AC.SyncCall("");
}