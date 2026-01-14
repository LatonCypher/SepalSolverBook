# Configuration file for the Sphinx documentation builder.

# -- Project information

project = 'Numerical Methods with SepalSolver'
copyright = '2025, CypherCrescent'
author = 'Lateef A. Kareem'

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
    'sphinxcontrib.mermaid'
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


# Custom configurations
html_logo = '_static/SepalSolver.png'

# 1. Add the path to your custom CSS
html_static_path = ['_static']
html_css_files = ['custom.css']

# 2. Define the .. terminal:: directive
from docutils import nodes
from docutils.parsers.rst import Directive

class TerminalDirective(Directive):
    has_content = True
    def run(self):
        text = '\n'.join(self.content)
        
        # Create the block
        node = nodes.literal_block(text, text)
        
        # ADD THESE TWO LINES:
        node['classes'].append('terminal')
        node['language'] = 'none'  # Forces Sphinx to skip syntax coloring
        
        return [node]

def setup(app):
    app.add_directive("terminal", TerminalDirective)
    # Ensure the CSS is registered
    app.add_css_file('custom.css')