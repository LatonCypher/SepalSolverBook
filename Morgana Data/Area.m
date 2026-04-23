function A = Area(P)
x = P(:,1); y = P(:,2);
x = [x; x(1)]; y = [y; y(1)];
A = 0.5*abs(sum(x(1:end-1).*y(2:end) - x(2:end).*y(1:end-1)));