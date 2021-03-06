<?xml version="1.0" encoding="utf-16" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xhtml="http://www.w3.org/1999/xhtml" exclude-result-prefixes="xhtml xsl">

    <xsl:output method="xml" />

    <!-- The root node -->
    <xsl:template match="/">
        <en-note>
            <xsl:apply-templates />
        </en-note>
    </xsl:template>

    <!-- The outer elements -->
    <xsl:template match="html|body">
        <xsl:apply-templates />
    </xsl:template>

    <!-- Ignore head and its descendants -->
    <xsl:template match="head" />

    <!-- Container of en-media and others -->
    <xsl:template match="embed">
        <en-media>
            <xsl:copy-of select="@type" />
        </en-media>
    </xsl:template>

    <!-- Supported tags and normalized to strip xhtml namespace -->
    <xsl:template
        match="xhtml:a|xhtml:abbr|xhtml:acronym|xhtml:address|xhtml:area|xhtml:b|xhtml:bdo|xhtml:big|xhtml:blockquote|xhtml:br|xhtml:caption|xhtml:center|xhtml:cite|xhtml:code|xhtml:col|xhtml:colgroup|xhtml:dd|xhtml:del|xhtml:dfn|xhtml:div|xhtml:dl|xhtml:dt|xhtml:em|xhtml:en-crypt|xhtml:en-media|xhtml:en-todo|xhtml:font|xhtml:h1|xhtml:h2|xhtml:h3|xhtml:h4|xhtml:h5|xhtml:h6|xhtml:hr|xhtml:i|xhtml:img|xhtml:ins|xhtml:kbd|xhtml:li|xhtml:map|xhtml:ol|xhtml:p|xhtml:pre|xhtml:q|xhtml:s|xhtml:samp|xhtml:small|xhtml:span|xhtml:strike|xhtml:strong|xhtml:sub|xhtml:sup|xhtml:table|xhtml:tbody|xhtml:td|xhtml:tfoot|xhtml:th|xhtml:thead|xhtml:tr|xhtml:tt|xhtml:u|xhtml:ul|xhtml:var">
        <xsl:element name="{name()}">
			<xsl:copy-of select="
				(@*[local-name()!='id'][local-name()!='scope'][local-name()!='class'])"/>
			<xsl:apply-templates />
        </xsl:element>
    </xsl:template>

    <!-- Strip unknown text -->
    <xsl:template match="*">
        <xsl:apply-templates />
    </xsl:template>

    <!-- Accept all text nodes -->
    <xsl:template match="text()">
        <xsl:copy>
            <xsl:copy-of select="@*" />
            <xsl:apply-templates />
        </xsl:copy>
    </xsl:template>


</xsl:stylesheet>
