USE QLSinhVien;
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
        RAISERROR(N'Lỗi: Mã lớp không tồn tại!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
    BEGIN
        RAISERROR(N'Lỗi: Mã sinh viên đã tồn tại!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM SinhVien WHERE Email = @Email)
    BEGIN
        RAISERROR(N'Lỗi: Email đã được sử dụng!', 16, 1);
        RETURN;
    END

    BEGIN TRY
        INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, MaLop)
        VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @MaLop);

        PRINT N'Thêm sinh viên thành công!';
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
        RAISERROR(N'Mã lớp không tồn tại trong hệ thống!', 16, 1);
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