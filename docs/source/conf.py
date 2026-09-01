# Configuration file for the Sphinx documentation builder.
from docutils import nodes
from docutils.parsers.rst import Directive

# -- Project information

project = 'CypherCrescent'
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
    'sphinx_design',
]

intersphinx_mapping = {
    'python': ('https://docs.python.org/3/', None),
    'sphinx': ('https://www.sphinx-doc.org/en/master/', None),
}
intersphinx_disabled_domains = ['std']

templates_path = ['_templates', '../_templates']

# -- Options for HTML output

html_theme = 'sphinx_rtd_theme'

# Disable Pygments theme backgrounds so custom CSS handles code panel styling
pygments_style = 'none'

# -- Options for EPUB output
epub_show_urls = 'footnote'

# -- Static files and Custom CSS

html_static_path = ['_static', '../_static']

html_css_files = [
    'custom.css',
]

# -- Custom Terminal Directive

class TerminalDirective(Directive):
    has_content = True
    def run(self):
        text = '\n'.join(self.content)
        node = nodes.literal_block(text, text)
        node['classes'].append('terminal')
        node['language'] = 'none'
        return [node]

def setup(app):
    app.add_directive("terminal", TerminalDirective)
