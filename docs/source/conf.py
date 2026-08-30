# Configuration file for the Sphinx documentation builder.

# -- Project information

project = 'CypherCresent'
copyright = '2025, CypherCrescent'
author = 'CypherCrescent'

release = '0.1'
version = '0.1.0'

# -- General configuration

extensions = [
    'sphinx.ext.duration',
    'sphinx.ext.doctest',
    'sphinx.ext.autodoc',
    'sphinx.ext.autosummary',
    'sphinx.ext.intersphinx',
    'sphinx.ext.mathjax',
    'sphinx_tabs.tabs',
    'sphinx.ext.autosectionlabel',
    'sphinx_copybutton',
    'sphinx.design'
]

intersphinx_mapping = {
    'python': ('https://docs.python.org/3/', None),
    'sphinx': ('https://www.sphinx-doc.org/en/master/', None),
}
intersphinx_disabled_domains = ['std']

templates_path = ['_templates']

# -- Options for HTML output

html_theme = 'sphinx_rtd_theme'

# -- Options for EPUB output
epub_show_urls = 'footnote'

# -- Options for Pygment style
pygments_style = 'sphinx'


# Ensure _static directory is included
html_static_path = ['_static']

# Set pygments_style to 'none' so Sphinx stops hardcoding light backgrounds
pygments_style = 'none'

# Register custom CSS
html_css_files = [
    'custom.css',
]

# Ensure custom.css is appended after all theme stylesheets
def setup(app):
    app.add_directive("terminal", TerminalDirective)
    app.add_css_file('custom.css', priority=999)  # Priority 999 forces it to load LAST