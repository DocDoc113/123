CREATE VIEW View_Book_Info AS
SELECT 
    S.Ma_Sach,
    DS.Ten_Dau_Sach,
    DS.Nam_Xuat_Ban,
    DS.Gia_Bia,
    TT.Ten_Tinh_Trang,
    TG.Ten_Tac_Gia,
    NXB.Ten_NXB,
    K.Ten_Kho,
    CD.Ten_Chu_De,
    TL.Ten_The_Loai
FROM 
    Sach S
INNER JOIN 
    DauSach DS ON S.Ma_Dau_Sach = DS.Ma_Dau_Sach
INNER JOIN 
    TinhTrang TT ON S.Ma_Tinh_Trang = TT.Ma_Tinh_Trang
INNER JOIN 
    TacGia_DauSach TDS ON DS.Ma_Dau_Sach = TDS.Ma_Dau_Sach
INNER JOIN 
    TacGia TG ON TDS.Ma_Tac_Gia = TG.Ma_Tac_Gia
INNER JOIN 
    NXB ON DS.Ma_NXB = NXB.Ma_NXB
INNER JOIN 
    Kho K ON DS.Ma_Kho = K.Ma_Kho
INNER JOIN 
    ChuDe CD ON DS.Ma_Chu_De = CD.Ma_Chu_De
INNER JOIN 
    TheLoai TL ON DS.Ma_TL = TL.Ma_The_Loai;
