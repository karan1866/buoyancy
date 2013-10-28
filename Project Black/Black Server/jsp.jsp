<%--
<%@ page import="java.io.*"%>
<%@ page import="java.net.*"%>

<%
        StringBuffer sbf = new StringBuffer();
        //Access the page
        try {
                URL url = new URL("http://translate.google.com/translate_tts?q=Hello");
                BufferedReader in = new BufferedReader(new InputStreamReader(url.openStream()));
                String inputLine;
                while ( (inputLine = in.readLine()) != null) sbf.append(inputLine);
                in.close();
        } catch (MalformedURLException e) {
        } catch (IOException e) {
        }
%><%= sbf.toString() %>--%>

<%@ page language = "java" import = "java.net.*" import = "java.io.*" %>
<%
String search,mime;
	HttpSession ses = request.getSession(true);
	if(request.getParameter("url") == null)
		search = "http://www.theflexshow.com/blog/images/the_flex_show.jpg";
	else
		search =request.getParameter("url");

	if(request.getParameter("mimeType") == null)
			mime = "application/xml";
		else
			mime =request.getParameter("url");
	mime = mime.trim();
	InputStream resultInStream = null;
	OutputStream resultOutStream = response.getOutputStream();
%>
<% response.setContentType(mime); %>
<%
try
	 {
		 URL url = new URL(search);
		 //Proxy proxy = new Proxy(Proxy.Type.HTTP, new InetSocketAddress("www-proxy.us.oracle.com", 80));
		 resultInStream = url.openStream();
		 //resultInStream = url.openConnection(proxy).getInputStream();
		 byte[] buffer = new byte[4096];
		 int bytes_read;
		 while((bytes_read=resultInStream.read(buffer)) != -1)
		  {
			   resultOutStream.write(buffer, 0, bytes_read);
		  }
		  resultOutStream.flush();
		  resultOutStream.close();
		  resultInStream.close();
 	} catch (Exception e) {%><%= e%><%}
	   finally{ try {  resultOutStream.flush(); resultOutStream.close(); resultInStream.close();  } catch (Exception e) {%><%= e%><%}
 }%>