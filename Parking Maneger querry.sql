-- 기존 외래 키 제약 조건 삭제
ALTER TABLE Receipt DROP CONSTRAINT fk_vehicle;
ALTER TABLE StoreDiscount DROP CONSTRAINT fk_vehicle_discount;

-- 기존 테이블 삭제
DROP TABLE StoreDiscount CASCADE CONSTRAINTS;
DROP TABLE Receipt CASCADE CONSTRAINTS;
DROP TABLE Vehicle CASCADE CONSTRAINTS;
DROP TABLE ParkingSpot CASCADE CONSTRAINTS;

-- 기존 시퀀스 삭제
DROP SEQUENCE ReceiptSeq;
DROP SEQUENCE VehicleSeq;
DROP SEQUENCE DiscountSeq;

-- Vehicle ID 시퀀스 생성
CREATE SEQUENCE VehicleSeq START WITH 1 INCREMENT BY 1;

-- Vehicle 테이블 생성
CREATE TABLE Vehicle (
    vehicle_id NUMBER PRIMARY KEY,
    vehicle_number VARCHAR2(10),
    vehicle_type VARCHAR2(10) CHECK (vehicle_type IN ('Standard', 'Compact'))
);

-- ParkingSpot 테이블 생성
CREATE TABLE ParkingSpot (
    spot_number NUMBER PRIMARY KEY,
    is_disabled NUMBER(1) DEFAULT 0,
    is_occupied NUMBER(1) DEFAULT 0,
    vehicle_id NUMBER UNIQUE,
    vehicle_number VARCHAR2(10),
    CONSTRAINT fk_vehicle_spot FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

-- Receipt 테이블 생성
CREATE TABLE Receipt (
    receipt_id NUMBER PRIMARY KEY,
    vehicle_id NUMBER NOT NULL,
    vehicle_number VARCHAR2(10),
    parking_fee_before_discount NUMBER NOT NULL,
    discount_amount NUMBER DEFAULT 0,
    total_fee NUMBER NOT NULL,
    parking_duration NUMBER NOT NULL,
    start_time DATE,
    end_time DATE,
    store_name VARCHAR2(50), -- 매장 이름 저장 (외래 키 없이 단순 정보)
    CONSTRAINT fk_vehicle FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);


-- Receipt ID 시퀀스 생성
CREATE SEQUENCE ReceiptSeq START WITH 1 INCREMENT BY 1;

-- 세션 내 기본 날짜 형식을 설정하여 시/분/초까지 포함하도록 변경
ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY-MM-DD HH24:MI:SS';

-- StoreDiscount 테이블 생성
CREATE TABLE StoreDiscount (
    discount_id NUMBER PRIMARY KEY, -- StoreDiscount만의 PK로 사용
    vehicle_id NUMBER NOT NULL, -- Vehicle 테이블의 FK로 사용
    store_name VARCHAR2(50) NOT NULL,
    discount_percentage NUMBER DEFAULT 0,
    discount_condition NUMBER NOT NULL,
    discount_date DATE DEFAULT SYSDATE, -- 할인 날짜 기록
    CONSTRAINT fk_vehicle_discount FOREIGN KEY (vehicle_id) REFERENCES Vehicle(vehicle_id)
);

CREATE SEQUENCE DiscountSeq START WITH 1 INCREMENT BY 1;

-- ParkingSpot 초기 데이터 삽입
BEGIN
    FOR i IN 1..25 LOOP
        INSERT INTO ParkingSpot (spot_number, is_disabled) VALUES (i, 0);
    END LOOP;
    FOR i IN 26..30 LOOP
        INSERT INTO ParkingSpot (spot_number, is_disabled) VALUES (i, 1);
    END LOOP;
END;
/