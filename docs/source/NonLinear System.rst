NonLinear System
================

The SepalSolver function ``Fsolve`` is used to solve **systems of nonlinear equations**. 
It finds a vector :math:`\mathbf{x}` such that:

.. math::

   \mathbf{f}(\mathbf{x}) = \mathbf{0}


Unlike ``Fzero``, which works for single-variable equations, ``Fsolve`` is designed for multivariable problems.

Syntax
------
The basic syntax is:

.. code-block:: csharp
   
   x = Fsolve(fun, x0);

Where:

- ``fun`` : Function handle that returns a vector of equations.
- ``x0``  : Initial guess for the solution vector.


Just like the case of `Fzero`, we can use `SolverSet` to configure the solver and gain a window into what is going on under the hood. 

.. code-block:: csharp

   var options = SolverSet(Display: true);
   x = fsolve(fun, x0, options);


How fsolve Works
----------------
- ``fsolve`` uses iterative numerical methods such as:
- **Newton Raphson Algorithm** (default, robust for many problems).
- **Forward Differencing** for Numerical differentiation of the function
- **LU rank 1 update to directly update the LU factors reducing the neet for repeated factorization.
- It requires a **good initial guess** because nonlinear systems may have multiple solutions or none at all.

Examples
--------


.. Admonition:: Example 1 :  : Single Equation

   Solve: :math:`x^2 - 4 = 0`:
   
   
   .. code-block:: csharp
   
      double fun(double x) => x*Sin(x) - 0.5;
      double x0 = 1;
      double root = Fsolve(fun, x0);
      Console.WriteLine($"root = {root}");
   
   
   Ouput
   
   .. terminal::
   
      root = 0.7408409563908155
   


.. Admonition:: Example 2 :  System of Equations

   Solve the system:
   
   .. math::
   
      \begin{array}{c}
      3x_1 - \cos(x_2 x_3) - \cfrac{1}{2} = 0 \\
      x_1^2 - 81(x_2+0.1)^2 + \sin(x_3) + 1.06 = 0 \\ 
      e^{x_1x_2} +20x_3 + \cfrac{10\pi-3}{3} = 0
      \end{array}
   
   Where: :math:`x_0 = [0.1, 0.1, -0.1]^T`
   
   
   .. code-block:: csharp
   
      double[] fun(double[] x) => [3 * x[0] - Cos(x[1] * x[2]) - 0.5,
                                   x[0] * x[0] - 81*Pow(x[1] + 0.1, 2) + Sin(x[2]) + 1.06,
                                   Exp(-x[0] * x[1]) + 20 * x[2] + (10 * pi - 3) / 3];
      // set initial guess
      double[] x0 = [0.1, 0.1, -0.1];
   
      // call the solver
      var x = Fsolve(fun, x0);
   
      // display the result
      Console.WriteLine(x);
   
   
   Ouput
   
   .. terminal::
   
      
         0.5000
         0.0000
        -0.5236
      
   
   Just like the case of single variable nonlinear equation, nonlinear system can also be solved using automatic differentiation class
   
   
   .. code-block:: csharp
   
   
      AutoDiff[] fun(AutoDiff[] x) => [3 * x[0] - Cos(x[1] * x[2]) - 0.5,
                           x[0] * x[0] - 81*Pow(x[1] + 0.1, 2) + Sin(x[2]) + 1.06,
                           Exp(-x[0] * x[1]) + 20 * x[2] + (10 * pi - 3) / 3];
      // set initial guess
      double[] x0 = [0.1, 0.1, -0.1];
   
      // call the solver
      var opts = SolverSet(Display: true);
      var x = Fsolve(fun, x0, opts);
   
      // display the result
      Console.WriteLine(x);
   
   
   Ouput
   
   .. terminal::
   
       Iteration    Func-count       f(x)      Norm of Step
           0            1             0           Start
           1            2          0.34586       0.58656     
           2            3          0.02588       0.01799     
           3            4        2.012e-004      0.00157     
           4            5        1.254e-008     1.245e-005   
           5            6        1.776e-015     7.761e-010   
           6            7        1.790e-015     1.111e-016   
           7            8        1.790e-015     1.125e-016   
           8            9        1.790e-015     1.125e-016   
           9            10       1.790e-015     1.125e-016   
           10           11       1.790e-015     1.125e-016   
           11           12       1.790e-015     1.125e-016   
           12           13       1.790e-015     1.125e-016   
           13           14       1.790e-015     1.125e-016   
           14           15       1.790e-015     1.125e-016   
           15           16       1.790e-015     1.125e-016   
           16           17       1.790e-015     1.125e-016   
           17           18       1.790e-015     1.125e-016   
           18           19       1.790e-015     1.125e-016   
           19           20       1.790e-015     1.125e-016   
           20           21       1.790e-015     1.125e-016   
           21           22       1.790e-015     1.125e-016   
           22           23       1.790e-015     1.125e-016   
           23           24       1.790e-015     1.125e-016   
           24           25       1.790e-015     1.125e-016   
           25           26       1.790e-015     1.125e-016   
           26           27       1.790e-015     1.125e-016   
           27           28       1.790e-015     1.125e-016   
           28           29       1.790e-015     1.125e-016   
           29           30       1.790e-015     1.125e-016   
           30           31       1.790e-015     1.125e-016   
           31           32       1.790e-015     1.125e-016   
           32           33       1.790e-015     1.125e-016   
           33           34       1.790e-015     1.125e-016   
           34           35       1.790e-015     1.125e-016   
           35           36       1.790e-015     1.125e-016   
           36           37       1.790e-015     1.125e-016   
           37           38       1.790e-015     1.125e-016   
           38           39       1.790e-015     1.125e-016   
           39           40       1.790e-015     1.125e-016   
           40           41       1.790e-015     1.125e-016   
           41           42       1.790e-015     1.125e-016   
           42           43       1.790e-015     1.125e-016   
           43           44       1.790e-015     1.125e-016   
           44           45       1.790e-015     1.125e-016   
           45           46       1.790e-015     1.125e-016   
           46           47       1.790e-015     1.125e-016   
           47           48       1.790e-015     1.125e-016   
           48           49       1.790e-015     1.125e-016   
           49           50       1.790e-015     1.125e-016   
           50           51       1.790e-015     1.125e-016   
           51           52       1.790e-015     1.125e-016   
           52           53       1.790e-015     1.125e-016   
           53           54       1.790e-015     1.125e-016   
           54           55       1.790e-015     1.125e-016   
           55           56       1.790e-015     1.125e-016   
           56           57       1.790e-015     1.125e-016   
           57           58       1.790e-015     1.125e-016   
           58           59       1.790e-015     1.125e-016   
           59           60       1.790e-015     1.125e-016   
           60           61       1.790e-015     1.125e-016   
           61           62       1.790e-015     1.125e-016   
           62           63       1.790e-015     1.125e-016   
           63           64       1.790e-015     1.125e-016   
           64           65       1.790e-015     1.125e-016   
           65           66       1.790e-015     1.125e-016   
           66           67       1.790e-015     1.125e-016   
           67           68       1.790e-015     1.125e-016   
           68           69       1.790e-015     1.125e-016   
           69           70       1.790e-015     1.125e-016   
           70           71       1.790e-015     1.125e-016   
           71           72       1.790e-015     1.125e-016   
           72           73       1.790e-015     1.125e-016   
           73           74       1.790e-015     1.125e-016   
           74           75       1.790e-015     1.125e-016   
           75           76       1.790e-015     1.125e-016   
           76           77       1.790e-015     1.125e-016   
           77           78       1.790e-015     1.125e-016   
           78           79       1.790e-015     1.125e-016   
           79           80       1.790e-015     1.125e-016   
           80           81       1.790e-015     1.125e-016   
           81           82       1.790e-015     1.125e-016   
           82           83       1.790e-015     1.125e-016   
           83           84       1.790e-015     1.125e-016   
           84           85       1.790e-015     1.125e-016   
           85           86       1.790e-015     1.125e-016   
           86           87       1.790e-015     1.125e-016   
           87           88       1.790e-015     1.125e-016   
           88           89       1.790e-015     1.125e-016   
           89           90       1.790e-015     1.125e-016   
           90           91       1.790e-015     1.125e-016   
           91           92       1.790e-015     1.125e-016   
           92           93       1.790e-015     1.125e-016   
           93           94       1.790e-015     1.125e-016   
           94           95       1.790e-015     1.125e-016   
           95           96       1.790e-015     1.125e-016   
           96           97       1.790e-015     1.125e-016   
           97           98       1.790e-015     1.125e-016   
           98           99       1.790e-015     1.125e-016   
           99          100       1.790e-015     1.125e-016   
          100          101       1.790e-015     1.125e-016   
          101          102       1.790e-015     1.125e-016   
          102          103       1.790e-015     1.125e-016   
          103          104       1.790e-015     1.125e-016   
          104          105       1.790e-015     1.125e-016   
          105          106       1.790e-015     1.125e-016   
          106          107       1.790e-015     1.125e-016   
          107          108       1.790e-015     1.125e-016   
          108          109       1.790e-015     1.125e-016   
          109          110       1.790e-015     1.125e-016   
          110          111       1.790e-015     1.125e-016   
          111          112       1.790e-015     1.125e-016   
          112          113       1.790e-015     1.125e-016   
          113          114       1.790e-015     1.125e-016   
          114          115       1.790e-015     1.125e-016   
          115          116       1.790e-015     1.125e-016   
          116          117       1.790e-015     1.125e-016   
          117          118       1.790e-015     1.125e-016   
          118          119       1.790e-015     1.125e-016   
          119          120       1.790e-015     1.125e-016   
          120          121       1.790e-015     1.125e-016   
          121          122       1.790e-015     1.125e-016   
          122          123       1.790e-015     1.125e-016   
          123          124       1.790e-015     1.125e-016   
          124          125       1.790e-015     1.125e-016   
          125          126       1.790e-015     1.125e-016   
          126          127       1.790e-015     1.125e-016   
          127          128       1.790e-015     1.125e-016   
          128          129       1.790e-015     1.125e-016   
          129          130       1.790e-015     1.125e-016   
          130          131       1.790e-015     1.125e-016   
          131          132       1.790e-015     1.125e-016   
          132          133       1.790e-015     1.125e-016   
          133          134       1.790e-015     1.125e-016   
          134          135       1.790e-015     1.125e-016   
          135          136       1.790e-015     1.125e-016   
          136          137       1.790e-015     1.125e-016   
          137          138       1.790e-015     1.125e-016   
          138          139       1.790e-015     1.125e-016   
          139          140       1.790e-015     1.125e-016   
          140          141       1.790e-015     1.125e-016   
          141          142       1.790e-015     1.125e-016   
          142          143       1.790e-015     1.125e-016   
          143          144       1.790e-015     1.125e-016   
          144          145       1.790e-015     1.125e-016   
          145          146       1.790e-015     1.125e-016   
          146          147       1.790e-015     1.125e-016   
          147          148       1.790e-015     1.125e-016   
          148          149       1.790e-015     1.125e-016   
          149          150       1.790e-015     1.125e-016   
          150          151       1.790e-015     1.125e-016   
          151          152       1.790e-015     1.125e-016   
          152          153       1.790e-015     1.125e-016   
          153          154       1.790e-015     1.125e-016   
          154          155       1.790e-015     1.125e-016   
          155          156       1.790e-015     1.125e-016   
          156          157       1.790e-015     1.125e-016   
          157          158       1.790e-015     1.125e-016   
          158          159       1.790e-015     1.125e-016   
          159          160       1.790e-015     1.125e-016   
          160          161       1.790e-015     1.125e-016   
          161          162       1.790e-015     1.125e-016   
          162          163       1.790e-015     1.125e-016   
          163          164       1.790e-015     1.125e-016   
          164          165       1.790e-015     1.125e-016   
          165          166       1.790e-015     1.125e-016   
          166          167       1.790e-015     1.125e-016   
          167          168       1.790e-015     1.125e-016   
          168          169       1.790e-015     1.125e-016   
          169          170       1.790e-015     1.125e-016   
          170          171       1.790e-015     1.125e-016   
          171          172       1.790e-015     1.125e-016   
          172          173       1.790e-015     1.125e-016   
          173          174       1.790e-015     1.125e-016   
          174          175       1.790e-015     1.125e-016   
          175          176       1.790e-015     1.125e-016   
          176          177       1.790e-015     1.125e-016   
          177          178       1.790e-015     1.125e-016   
          178          179       1.790e-015     1.125e-016   
          179          180       1.790e-015     1.125e-016   
          180          181       1.790e-015     1.125e-016   
          181          182       1.790e-015     1.125e-016   
          182          183       1.790e-015     1.125e-016   
          183          184       1.790e-015     1.125e-016   
          184          185       1.790e-015     1.125e-016   
          185          186       1.790e-015     1.125e-016   
          186          187       1.790e-015     1.125e-016   
          187          188       1.790e-015     1.125e-016   
          188          189       1.790e-015     1.125e-016   
          189          190       1.790e-015     1.125e-016   
          190          191       1.790e-015     1.125e-016   
          191          192       1.790e-015     1.125e-016   
          192          193       1.790e-015     1.125e-016   
          193          194       1.790e-015     1.125e-016   
          194          195       1.790e-015     1.125e-016   
          195          196       1.790e-015     1.125e-016   
          196          197       1.790e-015     1.125e-016   
          197          198       1.790e-015     1.125e-016   
          198          199       1.790e-015     1.125e-016   
          199          200       1.790e-015     1.125e-016   
          200          201       1.790e-015     1.125e-016   
          201          202       1.790e-015     1.125e-016   
          202          203       1.790e-015     1.125e-016   
          203          204       1.790e-015     1.125e-016   
          204          205       1.790e-015     1.125e-016   
          205          206       1.790e-015     1.125e-016   
          206          207       1.790e-015     1.125e-016   
          207          208       1.790e-015     1.125e-016   
          208          209       1.790e-015     1.125e-016   
          209          210       1.790e-015     1.125e-016   
          210          211       1.790e-015     1.125e-016   
          211          212       1.790e-015     1.125e-016   
          212          213       1.790e-015     1.125e-016   
          213          214       1.790e-015     1.125e-016   
          214          215       1.790e-015     1.125e-016   
          215          216       1.790e-015     1.125e-016   
          216          217       1.790e-015     1.125e-016   
          217          218       1.790e-015     1.125e-016   
          218          219       1.790e-015     1.125e-016   
          219          220       1.790e-015     1.125e-016   
          220          221       1.790e-015     1.125e-016   
          221          222       1.790e-015     1.125e-016   
          222          223       1.790e-015     1.125e-016   
          223          224       1.790e-015     1.125e-016   
          224          225       1.790e-015     1.125e-016   
          225          226       1.790e-015     1.125e-016   
          226          227       1.790e-015     1.125e-016   
          227          228       1.790e-015     1.125e-016   
          228          229       1.790e-015     1.125e-016   
          229          230       1.790e-015     1.125e-016   
          230          231       1.790e-015     1.125e-016   
          231          232       1.790e-015     1.125e-016   
          232          233       1.790e-015     1.125e-016   
          233          234       1.790e-015     1.125e-016   
          234          235       1.790e-015     1.125e-016   
          235          236       1.790e-015     1.125e-016   
          236          237       1.790e-015     1.125e-016   
          237          238       1.790e-015     1.125e-016   
          238          239       1.790e-015     1.125e-016   
          239          240       1.790e-015     1.125e-016   
          240          241       1.790e-015     1.125e-016   
          241          242       1.790e-015     1.125e-016   
          242          243       1.790e-015     1.125e-016   
          243          244       1.790e-015     1.125e-016   
          244          245       1.790e-015     1.125e-016   
          245          246       1.790e-015     1.125e-016   
          246          247       1.790e-015     1.125e-016   
          247          248       1.790e-015     1.125e-016   
          248          249       1.790e-015     1.125e-016   
          249          250       1.790e-015     1.125e-016   
          250          251       1.790e-015     1.125e-016   
          251          252       1.790e-015     1.125e-016   
          252          253       1.790e-015     1.125e-016   
          253          254       1.790e-015     1.125e-016   
          254          255       1.790e-015     1.125e-016   
          255          256       1.790e-015     1.125e-016   
          256          257       1.790e-015     1.125e-016   
          257          258       1.790e-015     1.125e-016   
          258          259       1.790e-015     1.125e-016   
          259          260       1.790e-015     1.125e-016   
          260          261       1.790e-015     1.125e-016   
          261          262       1.790e-015     1.125e-016   
          262          263       1.790e-015     1.125e-016   
          263          264       1.790e-015     1.125e-016   
          264          265       1.790e-015     1.125e-016   
          265          266       1.790e-015     1.125e-016   
          266          267       1.790e-015     1.125e-016   
          267          268       1.790e-015     1.125e-016   
          268          269       1.790e-015     1.125e-016   
          269          270       1.790e-015     1.125e-016   
          270          271       1.790e-015     1.125e-016   
          271          272       1.790e-015     1.125e-016   
          272          273       1.790e-015     1.125e-016   
          273          274       1.790e-015     1.125e-016   
          274          275       1.790e-015     1.125e-016   
          275          276       1.790e-015     1.125e-016   
          276          277       1.790e-015     1.125e-016   
          277          278       1.790e-015     1.125e-016   
          278          279       1.790e-015     1.125e-016   
          279          280       1.790e-015     1.125e-016   
          280          281       1.790e-015     1.125e-016   
          281          282       1.790e-015     1.125e-016   
          282          283       1.790e-015     1.125e-016   
          283          284       1.790e-015     1.125e-016   
          284          285       1.790e-015     1.125e-016   
          285          286       1.790e-015     1.125e-016   
          286          287       1.790e-015     1.125e-016   
          287          288       1.790e-015     1.125e-016   
          288          289       1.790e-015     1.125e-016   
          289          290       1.790e-015     1.125e-016   
          290          291       1.790e-015     1.125e-016   
          291          292       1.790e-015     1.125e-016   
          292          293       1.790e-015     1.125e-016   
          293          294       1.790e-015     1.125e-016   
          294          295       1.790e-015     1.125e-016   
          295          296       1.790e-015     1.125e-016   
          296          297       1.790e-015     1.125e-016   
          297          298       1.790e-015     1.125e-016   
          298          299       1.790e-015     1.125e-016   
          299          300       1.790e-015     1.125e-016   
          300          301       1.790e-015     1.125e-016   
      
         0.5000
         0.0000
        -0.5236
      

Applications
------------
- Engineering: Nonlinear circuit analysis, chemical equilibrium.
- Physics: Solving coupled nonlinear equations in dynamics.
- Optimization: Finding stationary points of nonlinear functions.

Limitations
-----------
- Requires a **good initial guess**; poor guesses may lead to divergence.
- May converge to **local solutions** rather than global ones.
- Sensitive to scaling of equations.

Comparison with fzero
---------------------

.. list-table:: 
   :header-rows: 1

   * - Feature
     - ``fzero``
     - ``fsolve``
   * - Problem type
     - Single nonlinear equation
     - System of nonlinear equations
   * - Input
     - Function handle, scalar or interval
     - Function handle, vector initial guess
   * - Methods used
     - Bisection, secant, inverse quadratic interp
     - Newton-Raphson's method
   * - Output
     - Scalar root
     - Vector solution



Parameterized Equations
-----------------------
Parameterized nonlinear equations :math:`F(x, \lambda) = 0` are equations or systems of equations that depend on one or more parameters: :math:`\lambda`. They are widely used in mathematics, engineering, and economics to study how solutions change as parameters vary, enabling sensitivity analysis, bifurcation studies, and optimization.

This parameter(s) can be exploited to provide means to guarantee that a good initial guess can be estimated. For instance, some values of the parameter might help eliminate the nonlinearity of the system and hence, no guess is needed for the solution. Then variation of this parameter can then be used to move the solution :math:`x` gently to their values that corresponds to the orginally intended values of the parameter :math:`\lambda`.


.. Admonition:: Example 2 : 

   Consider this parameterized nonlinear system. The nonlinearity is controlled by parameter :math:`c`.
   
   
   .. math::
   
      \begin{array}{c}
      2x + y - \exp(-cx) = 0 \\
      -x + 2y - \exp(-cy) = 0
      \end{array}
   
   
   Setting :math:`c = 0`, turns this system into a linear system with solution of :math:`[x,y] = [0.2, 0.6]`
   Hence, we can gradually change :math:`c` from :math:`0` to :math:`20`, while solving for :math:`[x, y]`.
   
   .. code-block:: csharp
   
      // Parameterized nonlinear equations
      double[] paramfun(ColVec x, double c)
      {
          return [ 2*x[0] + x[1]  - Exp(-c*x[0]),
          -x[0] + 2*x[1]  - Exp(-c*x[1])];
      }
   
      // variatiob of c from 0 to 20.
      RowVec C = Linspace(0, 20, 200);
   
      // initial guess as solution of linear system when c = 0.
      ColVec x = new double[] { 0.2, 0.6 };
   
      // setting maximum iteration number
      var opts = SolverSet(MaxIter: 1000);
      Matrix X = C.Select(c => x = Fsolve(x => paramfun(x, c), x, opts)).ToList();
      Plot(C, X, Linewidth: 2);
      SaveAs("Parameterozed_Nonlinear_Equations.png");
   
   
   .. figure:: images/Parameterozed_Nonlinear_Equations.png
      :align: center
      :alt: Parameterozed_Nonlinear_Equations.png
   
   

Matrix Equation
---------------
The SepalSolver also allow for easy computation of matrix equations. For instance, we can easily compute the cuberoot of a matrix. :math:`x^3 = \begin{pmatrix} 1&2 \\ 3&4  \end{pmatrix}`;

.. Admonition:: Example 3 : 

   
   .. math::
   
      x^3 = \begin{pmatrix} 1&2 \\ 3&4  \end{pmatrix}
   
   
   .. code-block:: csharp
   
      // Solve Nonlinear System of Polynomials
      Matrix A = new double[,]
      {
          {1, 2},
          {3, 4}
      };
      var opts = SolverSet(Display: true);
      Matrix x = Fsolve(x => x*x*x - A, Ones(2, 2), opts);
      Console.WriteLine(x);
   
   
   Ouput
   
   .. terminal::
   
       Iteration    Func-count       f(x)      Norm of Step
           0            1          3.74165        start      
           1            6          0.94293       0.61237     
           2            7          2661960       6432.80     
           3            8          0.45614       6432.80     
           4            9          0.39097       0.05487     
           5            10         0.39548       0.03847     
           6            11         0.39690       0.00219     
           7            12         0.39702      1.712e-004   
           8            17         263.674       6.34236     
           9            18         0.37461       6.33363     
           10           19         0.37411       0.00901     
           11           20         3.91142       1.47633     
           12           21         0.35406       1.34995     
           13           22         0.31481       0.11135     
           14           23         1.75353       0.89775     
           15           24         0.22618       0.76114     
           16           29         0.13103       0.26771     
           17           30         0.03317       0.09820     
           18           31         0.00338       0.01983     
           19           32       1.047e-004      0.00225     
           20           33       3.140e-007     6.762e-005   
           21           34       2.926e-011     2.022e-007   
      
        -0.1291    0.8602
         1.2903    1.1612
      

Summary
-------
``Fsolve`` is SelapSolver’s go-to tool for solving nonlinear systems. It is powerful and flexible, but demands careful choice of initial guesses and problem formulation to ensure convergence.
