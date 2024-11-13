CREATE VIEW View_DauSach_Details AS
SELECT 
    DS.Ma_Dau_Sach,
    DS.Ten_Dau_Sach,
    DS.Nam_Xuat_Ban,
    DS.Gia_Bia,
    DS.Ma_NXB,
    NXB.Ten_NXB,
    DS.Ma_Kho,
    K.Ten_Kho,
    DS.Ma_Chu_De,
    CD.Ten_Chu_De,
    DS.Ma_TL,
    TL.Ten_The_Loai,
    TDS.Ma_Tac_Gia,
    TG.Ten_Tac_Gia
FROM 
    DauSach DS
LEFT JOIN 
    NXB ON DS.Ma_NXB = NXB.Ma_NXB
LEFT JOIN 
    Kho K ON DS.Ma_Kho = K.Ma_Kho
LEFT JOIN 
    ChuDe CD ON DS.Ma_Chu_De = CD.Ma_Chu_De
LEFT JOIN 
    TheLoai TL ON DS.Ma_TL = TL.Ma_The_Loai
LEFT JOIN 
    TacGia_DauSach TDS ON DS.Ma_Dau_Sach = TDS.Ma_Dau_Sach
LEFT JOIN 
    TacGia TG ON TDS.Ma_Tac_Gia = TG.Ma_Tac_Gia;
