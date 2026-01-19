
Polynomials
===========

While matrices and vectors are the core of linear algebra, many engineering problems—such as curve fitting, signal processing, and finding eigenvalues— revolve around Polynomials. 

In SepalSolver, we represent a polynomial as a specialized class that manages a collection of coefficients and provides methods for evaluation, fitting, convolution, deconvolution, differentiation, and integration of polynomials. 

            
A polynomial :math:`P(x)=a_0 x^n + a_1 x^{n-1} + a_2 x^{n - 2} + \cdots + a_n` is defined by its coefficients. In SepalSolver, we store these in a double[] array where the index corresponds to the power of x. This makes the "Degree" of the polynomial exactly coefficients.Length - 1. 
            




.. toctree::

   Polynomial Evaluation
   Polynomial Fitting
   Polynomial Arithmetics
   Polynomial Roots
   Polynomial Differentiation and Integration
