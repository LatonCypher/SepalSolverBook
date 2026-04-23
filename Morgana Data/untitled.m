% Parameters for the umbrella
r = linspace(0, 1, 30); % Radius
theta = linspace(0, 2*pi, 30); % Angle
[R, T] = meshgrid(r, theta); % Create a grid
X = R .* cos(T); % X coordinates
Y = R .* sin(T); % Y coordinates
Z = sqrt(1 - R.^2); % Z coordinates for the umbrella surface
% Plotting the umbrella surface
figure;
surf(X, Y, Z);
hold on;
% Plotting the umbrella handle
t = linspace(0, 1, 100);
plot3(0.1 * cos(2*pi*t), 0.1 * sin(2*pi*t), -t, 'k', 'LineWidth', 2);
% Adding labels and title
xlabel('X');
ylabel('Y');
zlabel('Z');
title('3D Umbrella in MATLAB');