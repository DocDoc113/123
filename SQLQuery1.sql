CREATE VIEW ThongTinSach AS
SELECT 
    S.Ma_Sach,
    S.Ma_Tinh_Trang,
    DS.Ma_Dau_Sach,
    DS.Ten_Dau_Sach,
    DS.Nam_Xuat_Ban,
    DS.Gia_Bia,
    TL.Ten_The_Loai,
    TG.Ten_Tac_Gia,
    NXB.Ten_NXB,
    CD.Ten_Chu_De,
    K.Ten_Kho
FROM 
    Sach S
JOIN 
    DauSach DS ON S.Ma_Dau_Sach = DS.Ma_Dau_Sach
LEFT JOIN 
    TacGia_DauSach TGD ON DS.Ma_Dau_Sach = TGD.Ma_Dau_Sach
LEFT JOIN 
    TacGia TG ON TGD.Ma_Tac_Gia = TG.Ma_Tac_Gia
LEFT JOIN 
    TheLoai TL ON DS.Ma_TL = TL.Ma_The_Loai
LEFT JOIN 
    NXB ON DS.Ma_NXB = NXB.Ma_NXB
LEFT JOIN 
    ChuDe CD ON DS.Ma_Chu_De = CD.Ma_Chu_De
LEFT JOIN 
    Kho K ON DS.Ma_Kho = K.Ma_Kho;
