<%@ page language = "java" import = "java.net.*" import = "java.io.*" import = "java.util.*" %>

<%



OutputStreamWriter osw = new OutputStreamWriter(response.getOutputStream());

try
{
osw.write("java.net.useSystemProxies" + String.valueOf(System.getProperty("java.net.useSystemProxies")) + "\n");
System.setProperty("java.net.useSystemProxies", "true");
System.out.println("detecting proxies" + "\n");
List l = null;
try {
    l = ProxySelector.getDefault().select(new URI("http://foo/bar"));
} 
catch (URISyntaxException e) {
    e.printStackTrace();
}
if (l != null) {
    for (Iterator iter = l.iterator(); iter.hasNext();) {
        java.net.Proxy proxy = (java.net.Proxy) iter.next();
        //System.out.println("proxy hostname : " + proxy.type());
        osw.write("proxy hostname : " + proxy.type() + "\n");

        InetSocketAddress addr = (InetSocketAddress) proxy.address();

        if (addr == null) {
                //System.out.println("No Proxy");
                osw.write("No Proxy" + "\n");
        } else {
                //System.out.println("proxy hostname : " + addr.getHostName());
                osw.write("proxy hostname : " + addr.getHostName() + "\n");
                //System.setProperty("http.proxyHost", addr.getHostName());
                //osw.write("http.proxyHost", addr.getHostName());
                //System.out.println("proxy port : " + addr.getPort());
                osw.write("proxy port : " + addr.getPort() + "\n");
                //System.setProperty("http.proxyPort", Integer.toString(addr.getPort()));
                //osw.write("http.proxyPort", Integer.toString(addr.getPort()));
        }
    }
    
    
} 

}catch(Exception ex)
{
}
finally{
	osw.close();
}

%>