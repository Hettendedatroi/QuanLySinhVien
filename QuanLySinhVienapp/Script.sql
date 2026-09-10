USE QLSinhVien;

CREATE TABLE LopHoc (
	MaLop VARCHAR(18) PRIMARY KEY,
	TenLop VARCHAR(100) NOT NULL,
	NamHoc VARCHAR(18) NOT NULL,
);

CREATE TABLE Sinhvien (
	MaSV VARCHAR(18) PRIMARY KEY,
	HoTen NVARCHAR(100) NOT NULL,
	NgaySinh DATE NULL,
	GioiTinh NVARCHAR(5) CHECK (GioiTinh IN (N'Nam',N'N')),
	Email VARCHAR(100) UNIQUE,
	MaLop VARCHAR(18) NOT NULL,
	CONSTRAINT FK_Sinhvien_LopHoc FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
);
GO

INSERT INTO LopHoc (MaLop, TenLop, NamHoc) VALUES
('CNT01', N'Công nghệ thông tin 1', N'2023-2027'),
('CNT02', N'Công nghệ thông tin 2', N'2022-2026'),
('KTP01', N'Kỹ thuật phần mềm 1', N'2023-2027');

-- Thêm Sinh viên
INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, MaLop) VALUES
('SV001', N'Trương An', '2005-03-15', N'Nam', 'an.nv@gmail.com', 'CNT01'),
('SV002', N'Trần Thị Bích', '2005-07-20', N'Nữ', 'bich.tt@gmail.com', 'CNT01'),
('SV003', N'Lê Hoàng Cường', '2004-11-05', N'Nam', 'cuong.lh@gmail.com', 'CNT02'),
('SV004', N'Phạm Minh Dung', '2005-01-12', N'Nữ', 'dung.pm@gmail.com', 'KTP01'),
('SV005', N'Hoàng Anh Dũng', '2004-09-09', N'Nam', 'dung.ha@gmail.com', 'CNT01');

GO


ALTER TABLE SinhVien 
DROP CONSTRAINT CK__Sinhvien__GioiTi__276EDEB3;
GO

ALTER TABLE SinhVien 
ADD CONSTRAINT CK_Sinhvien_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ'));
GO


CREATE OR ALTER VIEW v_DanhSachSinhVien AS
SELECT 
    sv.MaSV,
    sv.HoTen,
    sv.NgaySinh,
    sv.GioiTinh,
    sv.Email,
    lh.TenLop,
    lh.NamHoc
FROM SinhVien sv
JOIN LopHoc lh ON sv.MaLop = lh.MaLop;
GO

CREATE OR ALTER PROCEDURE sp_ThemSinhVien
    @MaSV VARCHAR(18),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @Email VARCHAR(100),
    @MaLop VARCHAR(18)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM LopHoc WHERE MaLop = @MaLop)
    BEGIN
        RAISERROR('Mã lớp không tồn tại!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
    BEGIN
        RAISERROR('Mã sinh viên đã tồn tại!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SinhVien WHERE Email = @Email)
    BEGIN
        RAISERROR('Email đã được sử dụng!', 16, 1);
        RETURN;
    END

    BEGIN TRY
        INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, MaLop)
        VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @MaLop);

        PRINT 'Thêm sinh viên thành công!';
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE sp_LayDanhSachSinhVienTheoLop
    @MaLop VARCHAR(18)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM LopHoc WHERE MaLop = @MaLop)
    BEGIN
        RAISERROR('Mã lớp không tồn tại!', 16, 1);
        RETURN;
    END

    SELECT 
        sv.MaSV,
        sv.HoTen,
        sv.NgaySinh,
        sv.GioiTinh,
        sv.Email,
        lh.TenLop,
        lh.NamHoc
    FROM SinhVien sv
    JOIN LopHoc lh ON sv.MaLop = lh.MaLop
    WHERE sv.MaLop = @MaLop;
END;
GO
EXEC sp_LayDanhSachSinhVienTheoLop @MaLop = 'CNT01';
EXEC sp_ThemSinhVien 
    @MaSV = 'SV006', 
    @HoTen = N'Nguyễn Văn A', 
    @NgaySinh = '2005-01-01', 
    @GioiTinh = N'Nam', 
    @Email = 'a.nv@gmail.com', 
    @MaLop = 'CNT01';