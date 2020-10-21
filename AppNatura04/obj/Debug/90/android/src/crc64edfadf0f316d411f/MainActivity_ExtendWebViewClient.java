package crc64edfadf0f316d411f;


public class MainActivity_ExtendWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("AppNaturaRevendedora.MainActivity+ExtendWebViewClient, AppNaturaRevendedora", MainActivity_ExtendWebViewClient.class, __md_methods);
	}


	public MainActivity_ExtendWebViewClient ()
	{
		super ();
		if (getClass () == MainActivity_ExtendWebViewClient.class)
			mono.android.TypeManager.Activate ("AppNaturaRevendedora.MainActivity+ExtendWebViewClient, AppNaturaRevendedora", "", this, new java.lang.Object[] {  });
	}


	public boolean shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1)
	{
		return n_shouldOverrideUrlLoading (p0, p1);
	}

	private native boolean n_shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
