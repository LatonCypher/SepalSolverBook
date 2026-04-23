fileList = dir('*.mat')';
Top = []; Base = [];
for file = fileList
    name = file.name;
    data = load(name);
    vars = fieldnames(data);
    varName = vars{1};
    data = data.(varName);
    parts = split(name, ["_", "."]);
    num = str2double(parts(2));
    data = [data, repmat(num, size(data, 1), 1)];
    if(parts(1) == "Top")
        Top = [Top; data];
    else
        Base = [Base; data];
    end
end
% 1. Open a file for writing
fileID = fopen('Top.txt', 'w');

% 2. Write the data
% The format '%.2f %.2f %.2f\n' specifies 3 columns with 2 decimals
fprintf(fileID, '%.2f  %.2f  %.2f\n', Top');

% 3. Close the file
fclose(fileID);


% 1. Open a file for writing
fileID = fopen('Base.txt', 'w');

% 2. Write the data
% The format '%.2f %.2f %.2f\n' specifies 3 columns with 2 decimals
fprintf(fileID, '%.2f  %.2f  %.2f\n', Base');

% 3. Close the file
fclose(fileID);