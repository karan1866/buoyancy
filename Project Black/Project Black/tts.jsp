<%@ page language = "java" import = "java.net.*" import = "java.io.*" %>
<%

	InputStream resultInStream = null;
	OutputStream resultOutStream = response.getOutputStream();

	String urlMain = "http://translate.google.com/translate_tts";
	String paramQ = request.getParameter("q");
	
	response.setContentType("audio/mpeg");
	
	if(paramQ!=null)
	{
		byte[] buffer = new byte[4096];
		int bytes_read;
	
		try
		{
			URL url = new URL(urlMain + "?q=" + paramQ); 
			//URLConnection conn = url.openConnection(proxy);
			URLConnection conn = url.openConnection();
			
			conn.setRequestProperty("Host", "translate.google.com");
			conn.setRequestProperty("User-Agent", "Mozilla/5.0 (Windows NT 5.1; rv:2.0) Gecko/20100101 Firefox/4.0");
			conn.setRequestProperty("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			
			resultInStream = conn.getInputStream();
			 
			while((bytes_read=resultInStream.read(buffer)) != -1)
			 	resultOutStream.write(buffer, 0, bytes_read);
		}
		catch(Exception ex)
		{
			%><%= ex%><%
		}
		finally
		{
			try {
				resultOutStream.flush();
				resultOutStream.close();
				resultInStream.close();
			} catch (IOException ex) {
				%><%= ex%><%
			}
		}
	}
%>