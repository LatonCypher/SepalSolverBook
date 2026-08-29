# Change pygments_style to 'none' so it stops forcing light backgrounds
pygments_style = 'none'

# Options for HTML output
html_theme = 'sphinx_rtd_theme'

# Custom configurations
html_logo = '_static/SepalSolver.png'

# Path to static files
html_static_path = ['_static']
html_css_files = [
    'custom.css',
]

# Register terminal directive
from docutils import nodes
from docutils.parsers.rst import Directive

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
    # Note: app.add_css_file('custom.css') is omitted here because 
    # html_css_files already registers custom.css globally.